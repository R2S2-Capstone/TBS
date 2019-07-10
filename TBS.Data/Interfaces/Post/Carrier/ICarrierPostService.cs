using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.Data.Interfaces.Post.Carrier
{
    public interface ICarrierPostService
    {
        Task<PaginatedPosts> GetAllActivePosts(PaginationModel paginationModel);

        Task<CarrierPost> GetPostByIdAsync(int id);
    }
}
