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
        // Note: this will get all bids for a shipper
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

        // Create a carrier bid
        public async Task<bool> CreateBidAsync(string userFirebaseId, CarrierCreateBidRequest request)
        {
            request.Bid.Shipper = _context.Shippers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            request.Bid.Post = _context.CarrierPosts.FirstOrDefault(p => p.Id == request.PostId);
            await _context.CarrierBids.AddAsync(request.Bid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        // Cancel a carrier bid
        public async Task<bool> UpdateBidAsync(UpdateBidRequest request)
        {
            var bid = _context.CarrierBids.First(p => p.Id == request.BidId);
            bid.BidStatus = request.Status;
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
