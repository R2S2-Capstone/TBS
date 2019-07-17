using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.User
{
    public interface IShipperService
    {
        Task<PaginatedPosts> GetAllUsersPosts(string userFirebaseId, PaginationModel model);

        Task<ShipperPost> GetPostById(int id);

        Task<bool> CreatePostAsync(string userFirebaseId, ShipperPost post);

        Task<bool> UpdatePostAsync(int id, ShipperPost post);

        Task<bool> DeletePostAsync(int id);
    }
}
