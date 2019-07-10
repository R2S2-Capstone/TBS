using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Interfaces.Post.Carrier;
using TBS.Data.Models;
using TBS.Data.Models.Post;
using TBS.Data.Models.Post.Carrier;

namespace TBS.Services.Posts.Carrier
{
    public class CarrierPostService : ICarrierPostService
    {
        private readonly DatabaseContext _context;

        public CarrierPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<IEnumerable<CarrierPost>> GetAllActivePosts(PaginationModel model)
        {
            return await _context.CarrierPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
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