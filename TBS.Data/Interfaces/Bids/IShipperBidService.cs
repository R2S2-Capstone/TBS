using System;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Data.Interfaces.Bids
{
    public interface IShipperBidService
    {
        Task<PaginatedShipperBids> GetAllBidsByPostIdAsync(string userFirebaseId, Guid postId, PaginationModel model);

        Task<PaginatedShipperBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model);

        Task<ShipperBid> GetBidByIdAsync(Guid bidId);

        Task<bool> UpdateBidAsync(UpdateBidRequest request);

        Task<bool> CreateBidAsync(string userFirebaseId, ShipperCreateBidRequest request);

        Task<bool> SendReminderAsync(Guid id);
    }
}
