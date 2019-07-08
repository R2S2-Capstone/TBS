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
    public class CarrierPostService : ICarrierPostService
    {
        private readonly DatabaseContext _context;

        public CarrierPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<IEnumerable<CarrierPost>> GetAllPostsAsync()
        {
            return await _context.CarrierPosts.ToListAsync();
        }

        public async Task<IEnumerable<CarrierPost>> GetAllActivePostsAsync()
        {
            return await _context.CarrierPosts.Where(p => p.PostStatus == PostStatus.Open).ToListAsync();
        }

        public async Task<CarrierPost> GetPostByIdAsync(int id)
        {
            var carrierPost = await _context.CarrierPosts.FirstAsync(p => p.Id == id);

            if (carrierPost == null)
            {
                // TODO: implement exception
                throw new System.NotImplementedException();
            }

            return carrierPost;
        }

        public async Task<bool> CreatePostAsync(CarrierPost post)
        {
            await _context.CarrierPosts.AddAsync(post);
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

            _context.CarrierPosts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePostAsync(int id, CarrierPost post)
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
