using System.Threading.Tasks;
using TBS.Data.Models.Post.Request;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.User
{
    public interface IShipperService
    {
        Task<PaginatedPosts> GetAllUsersPosts(GetAllUsersPostsRequest request);

        Task<ShipperPost> GetPostById(GetPostByIdRequest request);

        Task<bool> CreateShipperPostAsync(ShipperPost post);

        Task<bool> UpdateShipperPostAsync(int id, ShipperPost post);

        Task<bool> RemoveShipperPostAsync(int id);
    }
}
