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
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Services.Bids
{
    public class ShipperBidService : IShipperBidService
    {
        private readonly DatabaseContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShipperBidService> _logger;

        public ShipperBidService(DatabaseContext databaseContext, IEmailService emailService, IConfiguration configuration, ILogger<ShipperBidService> logger)
        {
            _context = databaseContext;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
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
                .OrderByDescending(p => p.BidStatus)
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
                .OrderByDescending(p => p.BidStatus)
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

            if (request.Bid.Post.PostStatus != Data.Models.Posts.PostStatus.Open)
            {
                throw new FailedToUpdateBidException("Post is no longer accepting bids");
            }

            await _context.ShipperBids.AddAsync(request.Bid);
            await _context.SaveChangesAsync();

            //TODO: Make email prettier
            await _emailService.SendEmailAsync(
                request.Bid.Post.Shipper.Name,
                request.Bid.Post.Shipper.Email,
                $"New bid placed on {request.Bid.Post.PickupLocation.City} -> {request.Bid.Post.DropoffLocation.City}",
                $"A new bid has been placed on your post for ${request.Bid.BidAmount} from {request.Bid.Carrier.Name}<br>" +
                $"To view it click <a href='{_configuration["URL"]}/Shipper/Bids/{request.Bid.Post.Id}'>here</a><br>" +
                "Thanks,<br>" +
                "TBS Inc."
            );

            _logger.LogInformation($"Shipper Bid: Successfully created a new bid on {request.Bid.Post.PickupLocation.City} -> {request.Bid.Post.DropoffLocation.City}. From {request.Bid.Carrier.Name} for ${request.Bid.BidAmount}");

            return await Task.FromResult(true);
        }

        // Update a bid on a shipper post
        public async Task<bool> UpdateBidAsync(UpdateBidRequest request)
        {
            var bid = _context.ShipperBids
                .Include(b => b.Post)
                .Include(b => b.Post.PickupLocation)
                .Include(b => b.Post.DropoffLocation)
                .Include(b => b.Post.Shipper)
                .Include(b => b.Post.Vehicle)
                .Include(b => b.Carrier)
                .First(p => p.Id == request.BidId);

            // Make sure the post is not closed
            if (bid.Post.PostStatus == Data.Models.Posts.PostStatus.Closed)
            {
                throw new FailedToUpdateBidException("Post is no longer accepting bids");
            }
            else
            {
                bid.BidStatus = request.Status;

                _context.ShipperBids.Update(bid);

                // A shipper post can only have one accepted bid so make sure it is moved into the next state
                if (request.Status == Data.Models.Bids.BidStatus.PendingDelivery)
                {
                    bid.Post.PostStatus = Data.Models.Posts.PostStatus.PendingDelivery;
                    //TODO: Make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Carrier.Name,
                        bid.Carrier.Email,
                        $"Bid has been accepted on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City}",
                        $"Your bid has been accepted!<br>" +
                        $"View the delivery page <a href='{_configuration["URL"]}/Delivery/Shipper/{bid.Post.Id}/{bid.Id}'>here</a><br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );

                    // Save the changes so it's not considered a pending bid
                    await _context.SaveChangesAsync();

                    // Cancel all other bids
                    var pendingBids = _context.ShipperBids
                        .Include(b => b.Post)
                        .Where(b => b.Post.Id == bid.Post.Id && b.BidStatus == Data.Models.Bids.BidStatus.Open);

                    foreach (var pendingBid in pendingBids)
                    {
                        pendingBid.BidStatus = Data.Models.Bids.BidStatus.Declined;
                        _context.ShipperBids.Update(pendingBid);

                        //TODO: Make email prettier
                        await _emailService.SendEmailAsync(
                            pendingBid.Carrier.Name,
                            pendingBid.Carrier.Email,
                            $"Bid automatically cancelled on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City}",
                            $"Your bid has automatically been cancelled as the shipper has accepted another bid<br>" +
                            "Thanks,<br>" +
                            "TBS Inc."
                        );

                        _logger.LogInformation($"Shipper Bid: Automatically cancelled bid on {pendingBid.Post.PickupLocation.City} -> {pendingBid.Post.DropoffLocation.City} ({bid.Id}). From {pendingBid.Carrier.Name} for ${pendingBid.BidAmount}");
                    }

                    _logger.LogInformation($"Shipper Bid: Successfully accepted a bid on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City} ({bid.Id}). From {bid.Post.Shipper.Name} for ${bid.BidAmount}");
                }
                else if (request.Status == Data.Models.Bids.BidStatus.PendingDeliveryApproval)
                {
                    //TODO: Make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Post.Shipper.Name,
                        bid.Post.Shipper.Email,
                        $"{bid.Post.Vehicle.Year} {bid.Post.Vehicle.Make} {bid.Post.Vehicle.Model} has been delivered!",
                        $"Your vehicle has been delivered!<br>" +
                        $"Click <a href='{_configuration["URL"]}/Delivery/Shipper/{bid.Post.Id}/{bid.Id}'>here</a> to confirm delivery<br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );
                    _logger.LogInformation($"Shipper Bid: Vehicle has been delivered for bid on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City} ({bid.Id}). From {bid.Post.Shipper.Name} for ${bid.BidAmount}");

                }
                else if (request.Status == Data.Models.Bids.BidStatus.Completed)
                {
                    bid.Post.PostStatus = Data.Models.Posts.PostStatus.Closed;
                    //TODO: Make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Carrier.Name,
                        bid.Carrier.Email,
                        $"{bid.Post.Vehicle.Year} {bid.Post.Vehicle.Make} {bid.Post.Vehicle.Model} delivery has been confirmed!",
                        $"Your delivery has been confirmed!<br>" +
                        $"Click <a href='{_configuration["URL"]}/Delivery/Shipper/{bid.Post.Id}/{bid.Id}'>here</a> to add a rating!<br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );
                    _logger.LogInformation($"Shipper Bid: Delivery has been confirmed for bid on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City} ({bid.Id}). From {bid.Post.Shipper.Name} for ${bid.BidAmount}");

                }
                // Any other bid updates
                else
                {
                    //TODO: Make email prettier
                    await _emailService.SendEmailAsync(
                        bid.Carrier.Name,
                        bid.Carrier.Email,
                        $"Bid updated on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City}",
                        $"Your bid has been updated to {bid.BidStatus.ToString().ToLower()}<br>" +
                        $"Click <a href='{_configuration["URL"]}/Carrier'>here</a> and look under the 'Manage My Bids' section<br>" +
                        "Thanks,<br>" +
                        "TBS Inc."
                    );

                    _logger.LogInformation($"Shipper Bid: Updated bid to {bid.BidStatus} for {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City} ({bid.Id}). From {bid.Carrier.Name} for ${bid.BidAmount}");
                }

                await _context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<bool> SendReminderAsync(Guid id)
        {
            var bid = await GetBidByIdAsync(id);
            await _emailService.SendEmailAsync(
                bid.Post.Shipper.Name,
                bid.Post.Shipper.Email,
                $"REMINDER: Please confirm your delivery ({bid.Post.Vehicle.Year} {bid.Post.Vehicle.Make} {bid.Post.Vehicle.Model})",
                $"Your vehicle has been delivered. Please confirm your delivery!<br>" +
                $"Click <a href='{_configuration["URL"]}/Delivery/Shipper/{bid.Post.Id}/{bid.Id}'>here</a> to confirm delivery!<br>" +
                "Thanks,<br>" +
                "TBS Inc."
            );
            _logger.LogInformation($"Shipper Bid: Reminder has been sent for bid on {bid.Post.PickupLocation.City} -> {bid.Post.DropoffLocation.City} ({bid.Id}).");

            return await Task.FromResult(true);
        }
    }
}