using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Carrier;

namespace TBS.Data.Interfaces.Post.Carrier
{
    public interface ICarrierPostService
    {
        Task<IEnumerable<CarrierPost>> GetAllActivePosts(PaginationModel model);

        Task<CarrierPost> GetPostByIdAsync(int id);
    }
}
