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

        Task<bool> CreatePostAsync(CarrierPost post);

        Task<bool> UpdatePostAsync(int id, CarrierPost post);

        Task<bool> DeletePostAsync(int id);
    }
}
