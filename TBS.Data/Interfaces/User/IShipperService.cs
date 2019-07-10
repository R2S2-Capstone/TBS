using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models.Post.Request;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.User
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperPost>> GetAllPosts(GetAllUsersPostsRequest request);

        Task<ShipperPost> GetPostById(int id);

        Task<bool> CreateShipperPostAsync(ShipperPost post);

        Task<bool> UpdateShipperPostAsync(int id, ShipperPost post);

        Task<bool> RemoveShipperPostAsync(int id);
    }
}
