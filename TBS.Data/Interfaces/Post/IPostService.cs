using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;

namespace TBS.Data.Interfaces.Post
{
    public interface IPostService
    {
        Task<PaginatedCarrierPosts> GetAllActiveCarrierPosts(PaginationModel paginationModel);

        Task<PaginatedShipperPosts> GetAllActiveShipperPosts(PaginationModel paginationModel);
    }
}
