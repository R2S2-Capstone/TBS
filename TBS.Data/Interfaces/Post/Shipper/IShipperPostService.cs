using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Interfaces.Post.Shipper
{
    public interface IShipperPostService
    {
        Task<PaginatedPosts> GetAllActivePosts(PaginationModel paginationModel);

        Task<ShipperPost> GetPostByIdAsync(int id);
    }
}
