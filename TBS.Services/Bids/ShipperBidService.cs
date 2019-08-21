using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Bids;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Interfaces.Notifications;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Services.Bids
{
    public class ShipperBidService : IShipperBidService
    {
        private readonly DatabaseContext _context;
        private readonly IEmailService _emailService;

        public ShipperBidService(DatabaseContext databaseContext, IEmailService emailService)
        {
            _context = databaseContext;
            _emailService = emailService;
        }

        // Get a bid by id, used on details page
        public async Task<ShipperBid> GetBidByIdAsync(Guid bidId)
        {
            return await Task.FromResult(
                _context.ShipperBids
                .Include(b => b.Carrier)
                .Include(b => b.Post)
                .FirstOrDefault(b => b.Id == bidId)
            );
        }

        // Get all bids assoicated with a post given a post id, used on personal dashboard post detailed page
        public async Task<PaginatedShipperBids> GetAllBidsByPostIdAsync(string userFirebaseId, Guid postId, PaginationModel model)
        {
            var post = await _context.ShipperPosts
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Carrier)
                .FirstOrDefaultAsync(p => p.Id == postId);
            model.Count = post.Bids.Count();
            var paginatedBids = post.Bids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize)
                .OrderBy(p => p.BidStatus)
                .ToArray();
            return new PaginatedShipperBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Get all of your personal bids, used on personal dashboard
        // Note: this will get all bids for the carrier user
        public async Task<PaginatedShipperBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model)
        {
            var allBids = await _context.ShipperBids
                .Include(b => b.Carrier)
                .Include(b => b.Post)
                .Include(b => b.Post.PickupLocation)
                .Include(b => b.Post.DropoffLocation)
                .Where(b => b.Carrier.UserFirebaseId == userFirebaseId)
                .OrderBy(p => p.BidStatus)
                .ToListAsync();
            model.Count = allBids.Count();
            var paginatedBids = allBids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedShipperBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Create a bid on a shipper post
        public async Task<bool> CreateBidAsync(string userFirebaseId, ShipperCreateBidRequest request)
        {
            request.Bid.Carrier = _context.Carriers
                .FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            request.Bid.Post = _context.ShipperPosts
                .Include(p => p.Shipper)
                .Include(p => p.PickupLocation)
                .Include(p => p.DropoffLocation)
                .FirstOrDefault(p => p.Id == request.PostId);
            await _context.ShipperBids.AddAsync(request.Bid);
            await _context.SaveChangesAsync();

            //TODO: Add a link and make email prettier
            await _emailService.SendEmailAsync(
                request.Bid.Post.Shipper.Name,
                request.Bid.Post.Shipper.Email,
                $"New bid placed on {request.Bid.Post.PickupLocation.City} -> {request.Bid.Post.DropoffLocation.City}",
                $"A new bid has been placed on your post for ${request.Bid.BidAmount} from {request.Bid.Carrier.Name}<br>" +
                "Thanks,<br>" +
                "TBS Inc."
            );
            return await Task.FromResult(true);
        }

        // Update a bid on a shipper post
        public async Task<bool> UpdateBidAsync(UpdateBidRequest request)
        {
            var bid = _context.ShipperBids
                .Include(b => b.Post)
                .ThenInclude(p => p.PickupLocation)
                .Include(b => b.Post)
                .ThenInclude(p => p.DropoffLocation)
                .Include(b => b.Carrier)
                .First(p => p.Id == request.BidId);
            if (bid.Post.PostStatus != Data.Models.Posts.PostStatus.Open)
            {
                throw new FailedToUpdateBidException("Post is no longer accepting bids");
            }
            else
            {
                bid.BidStatus = request.Status;

                // A shipper post can only have one accepted bid so make sure it is moved into the next state
                if (bid.BidStatus == Data.Models.Bids.BidStatus.Approved)
                {
                    bid.Post.PostStatus = Data.Models.Posts.PostStatus.PendingDelivery;
                    //TODO: Add a link and make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Carrier.Name,
                        bid.Carrier.Email,
                        $"Bid has been accepted on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City}",
                        $"Your bid has been accepted!<br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );
                }
                else
                {
                    //TODO: Add a link and make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Carrier.Name,
                        bid.Carrier.Email,
                        $"Bid updated on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City}",
                        $"Your bid has been updated to {bid.BidStatus.ToString().ToLower()}.<br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );
                }

                _context.ShipperBids.Update(bid);

                await _context.SaveChangesAsync();

                // Cancel all other bids
                var pendingBids = _context.ShipperBids
                    .Include(b => b.Post)
                    .Where(b => b.Post.Id == bid.Post.Id && b.BidStatus == Data.Models.Bids.BidStatus.Open);

                foreach (var pendingBid in pendingBids)
                {
                    pendingBid.BidStatus = Data.Models.Bids.BidStatus.Declined;
                    _context.ShipperBids.Update(pendingBid);

                    //TODO: Add a link and make email prettier
                    await _emailService.SendEmailAsync(
                        pendingBid.Carrier.Name,
                        pendingBid.Carrier.Email,
                        $"Bid automatically cancelled on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City}",
                        $"Your bid has automatically been cancelled as the shipper has accepted another bid.<br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateBidException();
                }

                return await Task.FromResult(true);
            }
        }
    }
}
