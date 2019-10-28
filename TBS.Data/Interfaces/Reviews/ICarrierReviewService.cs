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
        Task<Array> GetAllReviewsByCarrierIdAsync(Guid carrierId);

        Task<bool> CreateReviewAsync(CarrierCreateReviewRequest request);
    }
}
