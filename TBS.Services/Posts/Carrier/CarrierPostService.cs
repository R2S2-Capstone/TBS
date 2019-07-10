using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Interfaces.Post.Carrier;
using TBS.Data.Models;
using TBS.Data.Models.Post;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.Services.Posts.Carrier
{
    public class CarrierPostService : ICarrierPostService
    {
        private readonly DatabaseContext _context;

        public CarrierPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<PaginatedPosts> GetAllActivePosts(PaginationModel paginationModel)
        {
            var allUserPosts = await _context.CarrierPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
            var orderedPosts = allUserPosts.OrderBy(p => p.PostStatus);
            paginationModel.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((paginationModel.CurrentPage - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize).ToList();
            return new PaginatedPosts() { PaginationModel = paginationModel, Posts = paginatedPosts };
        }

        public async Task<CarrierPost> GetPostByIdAsync(int id)
        {
            var carrierPost = await _context.CarrierPosts.FirstOrDefaultAsync(p => p.Id == id);

            if (carrierPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            return carrierPost;
        }
    }
}