using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Bids;
using TBS.Data.Exceptions.Bids.Shipper;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Services.Bids
{
    public class ShipperBidService : IShipperBidService
    {
        private readonly DatabaseContext _context;

        public ShipperBidService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        // Get a bid by id, used on details page
        public async Task<ShipperBid> GetBidByIdAsync(int bidId)
        {
            return await Task.FromResult(
                _context.ShipperBids
                .Include(b => b.Carrier)
                .Include(b => b.Post)
                .FirstOrDefault(b => b.Id == bidId)
            );
        }

        // Get all bids assoicated with a post given a post id, used on personal dashboard post detailed page
        public async Task<PaginatedShipperBids> GetAllBidsByPostIdAsync(string userFirebaseId, int postId, PaginationModel model)
        {
            var allBids = await _context.ShipperBids
                .Include(b => b.Carrier)
                .Include(b => b.Post)
                .Where(b => b.Post.Id == postId)
                .ToListAsync();
            model.Count = allBids.Count();
            var paginatedBids = allBids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedShipperBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Get all of your personal bids, used on personal dashboard
        // Note: this will get all bids for a carrier
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

        // Create a shipper bid
        public async Task<bool> CreateBidAsync(string userFirebaseId, ShipperCreateBidRequest request)
        {
            request.Bid.Carrier = _context.Carriers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            request.Bid.Post = _context.ShipperPosts.FirstOrDefault(p => p.Id == request.PostId);
            await _context.ShipperBids.AddAsync(request.Bid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        // Cancel a carrier bid
        public async Task<bool> UpdateBidAsync(UpdateBidRequest request)
        {
            var bid = _context.ShipperBids.First(p => p.Id == request.BidId);
            bid.BidStatus = request.Status;
            _context.ShipperBids.Update(bid);

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

        // Delete a shipper bid
        public async Task<bool> DeleteBidAsync(int bidId)
        {
            var shipperBid = await GetBidByIdAsync(bidId);

            if (shipperBid == null)
            {
                throw new InvalidShipperBidException();
            }

            _context.ShipperBids.Remove(shipperBid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
