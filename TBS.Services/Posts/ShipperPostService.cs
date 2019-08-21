using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts;
using TBS.Data.Exceptions.Posts.Shipper;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Models;
using TBS.Data.Models.Posts;
using TBS.Data.Models.Posts.Response;
using TBS.Data.Models.Posts.Shipper;

namespace TBS.Services.Post
{
    public class ShipperPostService : IShipperPostService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<ShipperPostService> _logger;

        public ShipperPostService(DatabaseContext databaseContext, ILogger<ShipperPostService> logger)
        {
            _context = databaseContext;
            _logger = logger;
        }

        public async Task<PaginatedShipperPosts> GetAllActivePostsAsync(PaginationModel model)
        {
            var allPosts = await _context.ShipperPosts
                .Include(p => p.Vehicle)
                .Include(p => p.PickupLocation)
                .Include(p => p.DropoffLocation)
                .Where(p => p.PostStatus == PostStatus.Open)
                .ToListAsync();
            model.Count = allPosts.Count();
            var paginatedPosts = allPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedShipperPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<PaginatedShipperPosts> GetAllUsersPostsAsync(string userFirebaseId, PaginationModel model)
        {
            var user = await _context.Shippers
                .Include(s => s.Posts)
                    .ThenInclude(p => p.Vehicle)
                .Include(s => s.Posts)
                    .ThenInclude(p => p.PickupLocation)
                .Include(s => s.Posts)
                    .ThenInclude(p => p.DropoffLocation)
                .FirstOrDefaultAsync(s => s.UserFirebaseId == userFirebaseId);
            var allUserPosts = user.Posts.ToList();
            var orderedPosts = allUserPosts.OrderBy(p => p.PostStatus);
            model.Count = orderedPosts.Count();
            var paginatedPosts = orderedPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();
            return new PaginatedShipperPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<ShipperPost> GetPostByIdAsync(Guid id)
        {
            var shipperPost = await _context.ShipperPosts
                .Include(p => p.Shipper)
                .Include(p => p.Shipper.Company)
                .Include(p => p.Shipper.Company.Address)
                .Include(p => p.Shipper.Company.Contact)
                .Include(p => p.Vehicle)
                .Include(p => p.PickupLocation)
                .Include(p => p.PickupContact)
                .Include(p => p.DropoffLocation)
                .Include(p => p.DropoffContact)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (shipperPost == null)
            {
                throw new InvalidShipperPostException();
            }

            return shipperPost;
        }

        public async Task<bool> CreatePostAsync(string userFirebaseId, ShipperPost post)
        {
            post.Shipper = _context.Shippers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);
            await _context.ShipperPosts.AddAsync(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Shipper Post: Successfully created a post {post.PickupLocation.City} -> {post.DropoffLocation.City}");

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePostAsync(Guid id, ShipperPost post)
        {
            if (id != post.Id)
            {
                throw new InvalidShipperPostException();
            }

            post.UpdatedOn = DateTime.Now;
            _context.ShipperPosts.Update(post);

            _logger.LogInformation($"Shipper Post: Successfully updated a post {post.PickupLocation.City} -> {post.DropoffLocation.City} ({post.Id})");

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

        public async Task<bool> DeletePostAsync(Guid id)
        {
            var post = await GetPostByIdAsync(id);

            if (post == null)
            {
                throw new InvalidShipperPostException();
            }

            _context.ShipperPosts.Remove(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Shipper Post: Successfully deleted a post {post.PickupLocation.City} -> {post.DropoffLocation.City}. ({post.Id})");

            return await Task.FromResult(true);
        }
    }
}