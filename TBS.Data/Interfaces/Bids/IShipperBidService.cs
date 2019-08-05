using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Response;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Data.Interfaces.Bids
{
    public interface IShipperBidService
    {
        Task<PaginatedShipperBids> GetAllBidsByPostIdAsync(string userFirebaseId, int postId, PaginationModel model);

        Task<PaginatedShipperBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model);

        Task<ShipperBid> GetBidByIdAsync(int bidId);

        Task<bool> CreateBidAsync(string userFirebaseId, ShipperBid bid);

        Task<bool> DeleteBidAsync(int bidId);
    }
}
