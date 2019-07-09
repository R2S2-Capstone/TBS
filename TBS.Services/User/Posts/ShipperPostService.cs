using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models.Post;

namespace TBS.Services.User.Posts
{
    public class ShipperPostService : IShipperPostService
    {
        private readonly DatabaseContext _context;

        public ShipperPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<IEnumerable<ShipperPost>> GetAllPostsAsync()
        {
            return await _context.ShipperPosts.ToListAsync();
        }

        public async Task<IEnumerable<ShipperPost>> GetAllActivePostsAsync()
        {
            return await _context.ShipperPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
        }

        public async Task<ShipperPost> GetPostByIdAsync(int id)
        {
            var carrierPost = await _context.ShipperPosts.FirstAsync(p => p.Id == id);

            if (carrierPost == null)
            {
                // TODO: implement exception
                throw new System.NotImplementedException();
            }

            return carrierPost;
        }

        public async Task<bool> CreatePostAsync(ShipperPost post)
        {
            await _context.ShipperPosts.AddAsync(post);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> RemovePostAsync(int id)
        {
            var carrierPost = await GetPostByIdAsync(id);

            if (carrierPost == null)
            {
                // TODO: implement exception
                throw new System.NotImplementedException();
            }

            _context.ShipperPosts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePostAsync(int id, ShipperPost post)
        {
            if (id != post.Id)
            {
                // TODO: implement exception
                throw new System.NotImplementedException();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // TODO: implement exception
                throw new System.NotImplementedException();
            }

            return await Task.FromResult(true);
        }
    }
}