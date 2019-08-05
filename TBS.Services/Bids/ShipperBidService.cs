using System.Threading.Tasks;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Response;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Services.Bids
{
    public class ShipperBidService : IShipperBidService
    {
        public Task<PaginatedShipperBids> GetAllBidsByPostId(string userFirebaseId, int postId, PaginationModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<PaginatedShipperBids> GetAllUsersPosts(string userFirebaseId, PaginationModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreateBidAsync(string userFirebaseId, ShipperBid bid)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteBidAsync(int bidId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ShipperBid> GetBidById(int bidId)
        {
            throw new System.NotImplementedException();
        }
    }
}
