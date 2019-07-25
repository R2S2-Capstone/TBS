using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.Services.Posts
{
    public class CarrierPostService : ICarrierPostService
    {
        private readonly DatabaseContext _context;

        public CarrierPostService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<PaginatedCarrierPosts> GetAllActivePosts(PaginationModel model)
        {
            var allPosts = await _context.CarrierPosts
                .Where(p => p.PostStatus == Data.Models.Post.PostStatus.Open)
                .ToListAsync();
            model.Count = allPosts.Count();
            var paginatedPosts = allPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedCarrierPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<PaginatedCarrierPosts> GetAllUsersPosts(string userFirebaseId, PaginationModel model)
        {
            var allUserPosts = await _context.CarrierPosts
                .Where(p => p.Carrier.UserFirebaseId == userFirebaseId)
                .ToListAsync();
            var orderedPosts = allUserPosts.OrderBy(p => p.PostStatus);
            model.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToList();
            return new PaginatedCarrierPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<CarrierPost> GetPostById(int id)
        {
            var carrierPost = await _context.CarrierPosts
                .FirstOrDefaultAsync(p => p.Id == id);

            if (carrierPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            return carrierPost;
        }

        public async Task<bool> CreatePostAsync(string userFirebaseId, CarrierPost post)
        {
            post.Carrier = _context.Carriers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            await _context.CarrierPosts.AddAsync(post);
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

            _context.CarrierPosts.Remove(carrierPost);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePostAsync(int id, CarrierPost post)
        {
            if (id != post.Id)
            {
                throw new InvalidCarrierPostException();
            }

            post.UpdatedOn = DateTime.Now;
            _context.CarrierPosts.Update(post);

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
