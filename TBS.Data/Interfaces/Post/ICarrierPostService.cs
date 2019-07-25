using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.Data.Interfaces.Post
{
    public interface ICarrierPostService
    {
        Task<PaginatedCarrierPosts> GetAllActivePosts(PaginationModel model);

        Task<PaginatedCarrierPosts> GetAllUsersPosts(string userFirebaseId, PaginationModel model);

        Task<CarrierPost> GetPostById(int postId);

        Task<bool> CreatePostAsync(string userFirebaseId, CarrierPost post);

        Task<bool> UpdatePostAsync(int id, CarrierPost post);

        Task<bool> DeletePostAsync(int id);
    }
}
