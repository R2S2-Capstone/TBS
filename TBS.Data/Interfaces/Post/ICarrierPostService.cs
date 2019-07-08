using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models.Post;

namespace TBS.Data.Interfaces.Post
{
    public interface ICarrierPostService
    {
        Task<IEnumerable<CarrierPost>> GetAllPostsAsync();

        Task<IEnumerable<CarrierPost>> GetAllActivePostsAsync();

        Task<CarrierPost> GetPostByIdAsync(int id);

        Task<bool> CreatePostAsync(CarrierPost post);

        Task<bool> UpdatePostAsync(CarrierPost post);

        Task<bool> RemovePostAsync(int id);
    }
}
