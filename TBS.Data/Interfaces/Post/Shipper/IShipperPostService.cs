using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.Post.Shipper
{
    public interface IShipperPostService
    {
        Task<IEnumerable<ShipperPost>> GetAllActivePosts(PaginationModel model);

        Task<ShipperPost> GetPostByIdAsync(int id);
    }
}
