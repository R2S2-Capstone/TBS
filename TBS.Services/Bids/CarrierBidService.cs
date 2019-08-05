using System.Threading.Tasks;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Response;

namespace TBS.Services.Bids
{
    public class CarrierBidService : ICarrierBidService
    {
        public Task<PaginatedCarrierBids> GetAllBidsByPostId(string userFirebaseId, int postId, PaginationModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<PaginatedCarrierBids> GetAllUsersPosts(string userFirebaseId, PaginationModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreateBidAsync(string userFirebaseId, CarrierBid bid)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteBidAsync(int bidId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CarrierBid> GetBidById(int bidId)
        {
            throw new System.NotImplementedException();
        }
    }
}
