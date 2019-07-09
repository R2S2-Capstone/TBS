using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.Post.Shipper
{
    public interface IShipperPostService
    {
        Task<IEnumerable<ShipperPost>> GetAllPostsAsync();

        Task<IEnumerable<ShipperPost>> GetAllActivePostsAsync();

        Task<ShipperPost> GetPostByIdAsync(int id);

        Task<bool> CreatePostAsync(ShipperPost post);

        Task<bool> UpdatePostAsync(int id, ShipperPost post);

        Task<bool> RemovePostAsync(int id);
    }
}
