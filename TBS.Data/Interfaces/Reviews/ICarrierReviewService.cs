using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Reviews;

namespace TBS.Data.Interfaces.Reviews
{
    public interface ICarrierReviewService
    {
        Task<PaginatedCarrierReviews> GetAllReviewsByCarrierIdAsync(Guid carrierId, PaginationModel paginationModel);

        Task<bool> CreateReviewAsync(CarrierCreateReviewRequest request);
    }
}
