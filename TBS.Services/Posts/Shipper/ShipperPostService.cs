using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts.Shipper;
using TBS.Data.Interfaces.Post.Shipper;
using TBS.Data.Models;
using TBS.Data.Models.Post;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Services.Posts.Shipper
{
    public class ShipperPostService : IShipperPostService
    {
        private readonly DatabaseContext _context;

        public ShipperPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<IEnumerable<ShipperPost>> GetAllActivePosts(PaginationModel model)
        {
            return await _context.ShipperPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
        }

        public async Task<ShipperPost> GetPostByIdAsync(int id)
        {
            var shipperPost = await _context.ShipperPosts.FirstOrDefaultAsync(p => p.Id == id);

            if (shipperPost == null)
            {
                throw new InvalidShipperPostException();
            }

            return shipperPost;
        }
    }
}