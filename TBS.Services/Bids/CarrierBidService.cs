using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Bids;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Interfaces.Notifications;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;

namespace TBS.Services.Bids
{
    public class CarrierBidService : ICarrierBidService
    {
        private readonly DatabaseContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CarrierBidService> _logger;

        public CarrierBidService(DatabaseContext databaseContext, IEmailService emailService, IConfiguration configuration, ILogger<CarrierBidService> logger)
        {
            _context = databaseContext;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

        // Get a bid by id, used on details page
        public async Task<CarrierBid> GetBidByIdAsync(Guid bidId)
        {
            return await Task.FromResult(
                _context.CarrierBids
                .Include(b => b.Shipper)
                .Include(b => b.Post)
                .Include(b => b.PickupLocation)
                .Include(b => b.Vehicle)
                .Include(b => b.DropoffLocation)
                .FirstOrDefault(b => b.Id == bidId)
            );
        }

        // Get all bids assoicated with a post given a post id, used on personal dashboard post detailed page
        public async Task<PaginatedCarrierBids> GetAllBidsByPostIdAsync(string userFirebaseId, Guid postId, PaginationModel model)
        {
            var post = await _context.CarrierPosts
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Shipper)
                .FirstOrDefaultAsync(p => p.Id == postId);
            model.Count = post.Bids.Count();
            var paginatedBids = post.Bids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize)
                .OrderBy(b => b.BidStatus)
                .ToArray();
            return new PaginatedCarrierBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Get all of your personal bids, used on personal dashboard
        // Note: this will get all bids for the shipper user
        public async Task<PaginatedCarrierBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model)
        {
            var allBids = await _context.CarrierBids
                .Include(b => b.Shipper)
                .Include(b => b.Post)
                .Where(b => b.Shipper.UserFirebaseId == userFirebaseId)
                .OrderBy(p => p.BidStatus)
                .ToListAsync();
            model.Count = allBids.Count();
            var paginatedBids = allBids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedCarrierBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Create a bid on a carrier post
        public async Task<bool> CreateBidAsync(string userFirebaseId, CarrierCreateBidRequest request)
        {
            request.Bid.Shipper = _context.Shippers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            request.Bid.Post = _context.CarrierPosts
                .Include(p => p.Carrier)
                .FirstOrDefault(p => p.Id == request.PostId);
            await _context.CarrierBids.AddAsync(request.Bid);
            await _context.SaveChangesAsync();

            //TODO: Make email prettier
            await _emailService.SendEmailAsync(
                request.Bid.Post.Carrier.Name,
                request.Bid.Post.Carrier.Email,
                $"New bid placed on {request.Bid.Post.PickupLocation} -> {request.Bid.Post.DropoffLocation}",
                $"A new bid has been placed on your post for ${request.Bid.BidAmount} from {request.Bid.Shipper.Name}<br>" +
                $"To view it click <a href='{_configuration["URL"]}/Carrier/ViewBid/{request.Bid.Id}'>here</a>" +
                "Thanks,<br>" + 
                "TBS Inc."
            );

            _logger.LogInformation($"Carrier Bid: Successfully created a new bid on {request.Bid.Post.PickupLocation} -> {request.Bid.Post.DropoffLocation}. From {request.Bid.Shipper.Name} for ${request.Bid.BidAmount}");

            return await Task.FromResult(true);
        }

        // Update a bid on a carrier post
        public async Task<bool> UpdateBidAsync(UpdateBidRequest request)
        {
            var bid = _context.CarrierBids
                .Include(b => b.Post)
                .ThenInclude(p => p.Carrier)
                .Include(b => b.Shipper)
                .First(p => p.Id == request.BidId);
            if (bid.Post.PostStatus != Data.Models.Posts.PostStatus.Open)
            {
                throw new FailedToUpdateBidException("Post is no longer accepting bids");
            }
            else
            {
                // A carrier post can have multiple accepted bids, make sure it is under the posted capacity
                var approvedBids = _context.CarrierBids
                    .Include(b => b.Post)
                    .Where(b => b.Post.Id == bid.Post.Id 
                        && (b.BidStatus == Data.Models.Bids.BidStatus.PendingDelivery 
                            || b.BidStatus == Data.Models.Bids.BidStatus.PendingDeliveryApproval 
                            || b.BidStatus == Data.Models.Bids.BidStatus.Completed)
                        )
                    .Count();
                // Spaces still available
                if (bid.Post.SpacesAvailable > approvedBids 
                        && (request.Status == Data.Models.Bids.BidStatus.PendingDelivery
                            || request.Status == Data.Models.Bids.BidStatus.PendingDeliveryApproval
                            || request.Status == Data.Models.Bids.BidStatus.Completed)
                        )
                {
                    bid.BidStatus = request.Status;

                    //TODO: Make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Shipper.Name,
                        bid.Shipper.Email,
                        $"Bid has been accepted on {bid.Post.PickupLocation} -> {bid.Post.DropoffLocation}",
                        $"Your bid has been accepted!<br>" +
                        $"View the delivery page <a href='{_configuration["URL"]}/Delivery/Carrier/{bid.Post.Id}/{bid.Id}'>here</a>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );

                    _logger.LogInformation($"Carrier Bid: Successfully accepted a bid on {bid.Post.PickupLocation} -> {bid.Post.DropoffLocation} ({bid.Id}). From {bid.Post.Carrier.Name} for ${bid.BidAmount}");


                    // The plus one reperesnts the just added approved bid
                    if (bid.Post.SpacesAvailable == approvedBids + 1)
                    {
                        // The post is at its maximum capacity so set it to a different state
                        bid.Post.PostStatus = Data.Models.Posts.PostStatus.PendingDelivery;

                        _context.CarrierBids.Update(bid);

                        await _context.SaveChangesAsync();

                        // Cancel all other bids
                        var pendingBids = _context.CarrierBids
                            .Include(b => b.Post)
                            .Include(b => b.Shipper)
                            .Where(b => b.Post.Id == bid.Post.Id && b.BidStatus == Data.Models.Bids.BidStatus.Open);

                        foreach (var pendingBid in pendingBids)
                        {
                            pendingBid.BidStatus = Data.Models.Bids.BidStatus.Declined;
                            _context.CarrierBids.Update(pendingBid);

                            //TODO: Add a link and make email prettier
                            await _emailService.SendEmailAsync(
                                pendingBid.Shipper.Name,
                                pendingBid.Shipper.Email,
                                $"Bid automatically cancelled on {bid.Post.PickupLocation} -> {bid.Post.DropoffLocation}",
                                $"Your bid has automatically been cancelled as the carrier has accepted the maximum number of bids.<br>" +
                                "Thanks,<br>" +
                                "TBS Inc."
                            );

                            _logger.LogInformation($"Carrier Bid: Automatically cancelled bid on {pendingBid.Post.PickupLocation} -> {pendingBid.Post.DropoffLocation} ({bid.Id}). From {pendingBid.Shipper.Name} for ${pendingBid.BidAmount}");
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                else if (bid.Post.SpacesAvailable <= approvedBids)
                {
                    throw new FailedToUpdateBidException("Already reached maximum accepted bids");
                }
                else
                {
                    bid.BidStatus = request.Status;

                    //TODO: Add a link and make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Shipper.Name,
                        bid.Shipper.Email,
                        $"Bid updated on {bid.Post.PickupLocation} -> {bid.Post.DropoffLocation}",
                        $"Your bid has been updated to {bid.BidStatus.ToString().ToLower()}.<br>" +
                        $"Click <a href='{_configuration["URL"]}/Shipper'>here</a> and look under the 'Manage My Bids' section" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );

                    _logger.LogInformation($"Carrier Bid: Updated bid to {bid.BidStatus} for {bid.Post.PickupLocation} -> {bid.Post.DropoffLocation} ({bid.Id}). From {bid.Shipper.Name} for ${bid.BidAmount}");
                }

                _context.CarrierBids.Update(bid);

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
