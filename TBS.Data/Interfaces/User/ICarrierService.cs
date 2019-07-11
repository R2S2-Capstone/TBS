using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.Data.Interfaces.User
{
    public interface ICarrierService
    {
        Task<PaginatedPosts> GetAllUsersPosts(string userFirebaseId, PaginationModel model);

        Task<CarrierPost> GetPostById(int postId);

        Task<bool> CreateCarrierPostAsync(CarrierPost post);

        Task<bool> UpdateCarrierPostAsync(int id, CarrierPost post);

        Task<bool> RemoveCarrierPostAsync(int id);
    }
}
