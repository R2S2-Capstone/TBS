using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Bids.Carrier;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
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
        public async Task<CarrierBid> GetBidByIdAsync(int bidId)
        {
            return await Task.FromResult(
                _context.CarrierBids
                .Include(b => b.Shipper)
                .FirstOrDefault(b => b.Id == bidId)
            );
        }

        // Get all bids assoicated with a post given a post id, used on personal dashboard post detailed page
        public async Task<PaginatedCarrierBids> GetAllBidsByPostIdAsync(string userFirebaseId, int postId, PaginationModel model)
        {
            var allBids = await _context.CarrierBids
                .Include(b => b.Shipper)
                .Where(b => b.Id == postId)
                .ToListAsync();
            model.Count = allBids.Count();
            var paginatedBids = allBids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedCarrierBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Get all of your personal bids, used on personal dashboard
        public async Task<PaginatedCarrierBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model)
        {
            var allBids = await _context.CarrierBids
                .Include(b => b.Shipper)
                .Where(b => b.Shipper.UserFirebaseId == userFirebaseId)
                .ToListAsync();
            model.Count = allBids.Count();
            var paginatedBids = allBids
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedCarrierBids() { PaginationModel = model, Bids = paginatedBids };
        }

        // Create a carrier bid
        public async Task<bool> CreateBidAsync(string userFirebaseId, CarrierBid bid)
        {
            bid.Shipper = _context.Shippers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            await _context.CarrierBids.AddAsync(bid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        // Delete a carrier bid
        public async Task<bool> DeleteBidAsync(int bidId)
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
