using System.Threading.Tasks;
using TBS.Data.Models.Reviews;

namespace TBS.Data.Interfaces.Reviews
{
    public interface IShipperReviewService
    {
        Task<bool> CreateReviewAsync(ShipperCreateReviewRequest request);
    }
}
