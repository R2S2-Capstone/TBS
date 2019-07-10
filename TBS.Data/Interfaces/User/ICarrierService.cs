using System.Threading.Tasks;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Request;
using TBS.Data.Models.Post.Response;

namespace TBS.Data.Interfaces.User
{
    public interface ICarrierService
    {
        Task<PaginatedPosts> GetAllUsersPosts(GetAllUsersPostsRequest request);

        Task<CarrierPost> GetPostById(GetPostByIdRequest request);

        Task<bool> CreateCarrierPostAsync(CarrierPost post);

        Task<bool> UpdateCarrierPostAsync(int id, CarrierPost post);

        Task<bool> RemoveCarrierPostAsync(int id);
    }
}
