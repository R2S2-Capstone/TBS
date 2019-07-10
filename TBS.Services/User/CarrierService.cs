using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Interfaces.User;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Request;
using TBS.Data.Models.Post.Response;

namespace TBS.Services.User
{
    public class CarrierService : ICarrierService
    {
        private readonly DatabaseContext _context;

        public CarrierService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<PaginatedPosts> GetAllUsersPosts(GetAllUsersPostsRequest request)
        {
            var allUserPosts = await _context.CarrierPosts.Where(p => p.Carrier.UserFirebaseId == request.UserFirebaseId).ToListAsync();
            var orderedPosts = allUserPosts.OrderBy(p => p.PostStatus);
            request.PaginationModel.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((request.PaginationModel.CurrentPage - 1) * request.PaginationModel.PageSize)
                .Take(request.PaginationModel.PageSize).ToList();
            return new PaginatedPosts() { PaginationModel = request.PaginationModel, Posts = paginatedPosts };
        }

        public async Task<CarrierPost> GetPostById(GetPostByIdRequest request)
        {
            // do other validation
            return await GetPostById(request.PostId);
        }

        public async Task<CarrierPost> GetPostById(int id)
        {
            var carrierPost = await _context.CarrierPosts.FirstOrDefaultAsync(p => p.Id == id);

            if (carrierPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            return carrierPost;
        }

        public async Task<bool> CreateCarrierPostAsync(CarrierPost post)
        {
            await _context.CarrierPosts.AddAsync(post);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> RemoveCarrierPostAsync(int id)
        {
            var carrierPost = await GetPostById(id);

            if (carrierPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            _context.CarrierPosts.Remove(await GetPostById(id));
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCarrierPostAsync(int id, CarrierPost post)
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
