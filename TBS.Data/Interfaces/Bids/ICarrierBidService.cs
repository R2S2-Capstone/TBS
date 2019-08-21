using System;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Response;

namespace TBS.Data.Interfaces.Bids
{
    public interface ICarrierBidService
    {
        Task<CarrierBid> GetBidByIdAsync(Guid bidId);

        Task<PaginatedCarrierBids> GetAllBidsByPostIdAsync(string userFirebaseId, Guid postId, PaginationModel model);

        Task<PaginatedCarrierBids> GetAllUsersBidsAsync(string userFirebaseId, PaginationModel model);

        Task<bool> UpdateBidAsync(UpdateBidRequest request);

        Task<bool> CreateBidAsync(string userFirebaseId, CarrierCreateBidRequest request);
    }
}
