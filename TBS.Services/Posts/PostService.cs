using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models;
using TBS.Data.Models.Post;
using TBS.Data.Models.Post.Response;

namespace TBS.Services.Posts.Carrier
{
    public class CarrierPostService : IPostService
    {
        private readonly DatabaseContext _context;

        public CarrierPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<PaginatedPosts> GetAllActiveCarrierPosts(PaginationModel paginationModel)
        {
            var allPosts = await _context.CarrierPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
            var orderedPosts = allPosts.OrderBy(p => p.PostStatus);
            paginationModel.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((paginationModel.CurrentPage - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize).ToList();
            return new PaginatedPosts() { PaginationModel = paginationModel, Posts = paginatedPosts };
        }

        public async Task<PaginatedPosts> GetAllActiveShipperPosts(PaginationModel paginationModel)
        {
            var allPosts = await _context.ShipperPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
            var orderedPosts = allPosts.OrderBy(p => p.PostStatus);
            paginationModel.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((paginationModel.CurrentPage - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize).ToList();
            return new PaginatedPosts() { PaginationModel = paginationModel, Posts = paginatedPosts };
        }
    }
}