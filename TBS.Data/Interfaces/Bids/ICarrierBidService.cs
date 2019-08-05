using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Response;

namespace TBS.Data.Interfaces.Bids
{
    public interface ICarrierBidService
    {
        Task<PaginatedCarrierBids> GetAllBidsByPostId(string userFirebaseId, int postId, PaginationModel model);

        Task<PaginatedCarrierBids> GetAllUsersPosts(string userFirebaseId, PaginationModel model);

        Task<CarrierBid> GetBidById(int bidId);

        Task<bool> CreateBidAsync(string userFirebaseId, CarrierBid bid);

        Task<bool> DeleteBidAsync(int bidId);
    }
}
