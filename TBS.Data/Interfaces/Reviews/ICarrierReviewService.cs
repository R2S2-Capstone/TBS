using System.Threading.Tasks;
using TBS.Data.Models.Reviews;

namespace TBS.Data.Interfaces.Reviews
{
    public interface ICarrierReviewService
    {
        Task<bool> CreateReviewAsync(CarrierCreateReviewRequest request);
    }
}
