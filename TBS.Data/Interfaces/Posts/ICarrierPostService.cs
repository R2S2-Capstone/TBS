using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Posts.Carrier;
using TBS.Data.Models.Posts.Response;

namespace TBS.Data.Interfaces.Posts
{
    public interface ICarrierPostService
    {
        Task<PaginatedCarrierPosts> GetAllActivePostsAsync(PaginationModel model);

        Task<PaginatedCarrierPosts> GetAllUsersPostsAsync(string userFirebaseId, PaginationModel model);

        Task<CarrierPost> GetPostByIdAsync(int postId);

        Task<bool> CreatePostAsync(string userFirebaseId, CarrierPost post);

        Task<bool> UpdatePostAsync(int id, CarrierPost post);

        Task<bool> DeletePostAsync(int id);
    }
}
