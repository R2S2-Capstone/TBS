using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.Post
{
    public interface IShipperPostService
    {
        Task<PaginatedShipperPosts> GetAllActivePosts(PaginationModel model);

        Task<PaginatedShipperPosts> GetAllUsersPosts(string userFirebaseId, PaginationModel model);

        Task<ShipperPost> GetPostById(int id);

        Task<bool> CreatePostAsync(string userFirebaseId, ShipperPost post);

        Task<bool> UpdatePostAsync(int id, ShipperPost post);

        Task<bool> DeletePostAsync(int id);
    }
}
