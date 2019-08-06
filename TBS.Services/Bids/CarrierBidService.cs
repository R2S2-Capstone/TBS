using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Bids;
using TBS.Data.Exceptions.Bids.Carrier;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;

namespace TBS.Services.Bids
{
    public class CarrierBidService : ICarrierBidService
    {
        private readonly DatabaseContext _context;

        public CarrierBidService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
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
            var allBids = await _context.CarrierBids
                .Include(b => b.Shipper)
                .Include(b => b.Post)
                .Where(b => b.Post.Id == postId)
                .ToListAsync();
            model.Count = allBids.Count();
            var paginatedBids = allBids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
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
            request.Bid.Post = _context.CarrierPosts.FirstOrDefault(p => p.Id == request.PostId);
            await _context.CarrierBids.AddAsync(request.Bid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        // Update a bid on a carrier post
        public async Task<bool> UpdateBidAsync(UpdateBidRequest request)
        {
            var bid = _context.CarrierBids
                .Include(b => b.Post)
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
                    .Where(b => b.Post.Id == bid.Post.Id && b.BidStatus == Data.Models.Bids.BidStatus.Approved)
                    .Count();
                // Spaces still available
                if (bid.Post.SpacesAvailable > approvedBids && request.Status == Data.Models.Bids.BidStatus.Approved)
                {
                    bid.BidStatus = request.Status;

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
                            .Where(b => b.Post.Id == bid.Post.Id && b.BidStatus == Data.Models.Bids.BidStatus.Open);

                        foreach (var pendingBid in pendingBids)
                        {
                            pendingBid.BidStatus = Data.Models.Bids.BidStatus.Declined;
                            _context.CarrierBids.Update(pendingBid);
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

        // Delete a carrier bid
        public async Task<bool> DeleteBidAsync(Guid bidId)
        {
            var carrierBid = await GetBidByIdAsync(bidId);

            if (carrierBid == null)
            {
                throw new InvalidCarrierBidException();
            }

            _context.CarrierBids.Remove(carrierBid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
