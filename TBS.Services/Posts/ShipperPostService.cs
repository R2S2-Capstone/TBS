using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Services.Posts
{
    public class ShipperPostService : IShipperPostService
    {
        private readonly DatabaseContext _context;

        public ShipperPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<PaginatedShipperPosts> GetAllUsersPosts(string userFirebaseId, PaginationModel model)
        {
            var allUserPosts = await _context.ShipperPosts
                .Include(p => p.Vehicle)
                .Include(p => p.PickupLocation)
                .Include(p => p.DropoffLocation)
                .Where(p => p.Shipper.UserFirebaseId == userFirebaseId)
                .ToListAsync();
            var orderedPosts = allUserPosts.OrderBy(p => p.PostStatus);
            model.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedShipperPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<ShipperPost> GetPostById(int id)
        {
            var shipperPost = await _context.ShipperPosts.FirstOrDefaultAsync(p => p.Id == id);

            if (shipperPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            return shipperPost;
        }

        public async Task<bool> CreatePostAsync(string userFirebaseId, ShipperPost post)
        {
            post.Shipper = _context.Shippers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            await _context.ShipperPosts.AddAsync(post);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var carrierPost = await GetPostById(id);

            if (carrierPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            _context.ShipperPosts.Remove(await GetPostById(id));
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePostAsync(int id, ShipperPost post)
        {
            if (id != post.Id)
            {
                throw new InvalidCarrierPostException();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new FailedToUpdatePostException();
            }

            return await Task.FromResult(true);
        }
    }
}