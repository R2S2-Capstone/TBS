using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models.Post;

namespace TBS.Data.Interfaces.Post
{
    public interface IShipperPostService
    {
        Task<IEnumerable<ShipperPost>> GetAllPostsAsync();

        Task<IEnumerable<ShipperPost>> GetAllActivePostsAsync();

        Task<ShipperPost> GetPostByIdAsync(int id);

        Task<bool> CreatePostAsync(ShipperPost post);

        Task<bool> UpdatePostAsync(ShipperPost post);

        Task<bool> RemovePostAsync(int id);
    }
}
