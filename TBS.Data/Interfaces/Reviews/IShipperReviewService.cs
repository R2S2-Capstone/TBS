using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Reviews;

namespace TBS.Data.Interfaces.Reviews
{
    public interface IShipperReviewService
    {

        Task<Array> GetAllReviewsByShipperIdAsync(Guid shipperId);

        Task<bool> CreateReviewAsync(ShipperCreateReviewRequest request);


    }
}
