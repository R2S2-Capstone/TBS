using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;

namespace TBS.Data.Interfaces.Post
{
    public interface IPostService
    {
        Task<PaginatedPosts> GetAllActiveCarrierPosts(PaginationModel paginationModel);

        Task<PaginatedPosts> GetAllActiveShipperPosts(PaginationModel paginationModel);
    }
}
