using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Request;

namespace TBS.Data.Interfaces.User
{
    public interface ICarrierService
    {
        Task<IEnumerable<CarrierPost>> GetAllPosts(GetAllUsersPostsRequest request);

        Task<CarrierPost> GetPostById(int id);

        Task<bool> CreateCarrierPostAsync(CarrierPost post);

        Task<bool> UpdateCarrierPostAsync(int id, CarrierPost post);

        Task<bool> RemoveCarrierPostAsync(int id);
    }
}
