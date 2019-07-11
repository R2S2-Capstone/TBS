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

        Task<bool> CreateShipperPostAsync(ShipperPost post);

        Task<bool> UpdateShipperPostAsync(int id, ShipperPost post);

        Task<bool> RemoveShipperPostAsync(int id);
    }
}
