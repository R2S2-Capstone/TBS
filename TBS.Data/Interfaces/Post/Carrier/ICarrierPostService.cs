using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models.Post.Carrier;

namespace TBS.Data.Interfaces.Post.Carrier
{
    public interface ICarrierPostService
    {
        Task<IEnumerable<CarrierPost>> GetAllPostsAsync();

        Task<IEnumerable<CarrierPost>> GetAllActivePostsAsync();

        Task<CarrierPost> GetPostByIdAsync(int id);

        Task<bool> CreatePostAsync(CarrierPost post);

        Task<bool> UpdatePostAsync(int id, CarrierPost post);

        Task<bool> RemovePostAsync(int id);
    }
}
