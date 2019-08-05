using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Response;

namespace TBS.Data.Interfaces.Bids
{
    public interface ICarrierBidService
    {
        Task<CarrierBid> GetBidByIdAsync(int bidId);

        Task<PaginatedCarrierBids> GetAllBidsByPostIdAsync(string userFirebaseId, int postId, PaginationModel model);

        Task<PaginatedCarrierBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model);

        Task<bool> CreateBidAsync(string userFirebaseId, CarrierBid bid);

        Task<bool> DeleteBidAsync(int bidId);
    }
}
