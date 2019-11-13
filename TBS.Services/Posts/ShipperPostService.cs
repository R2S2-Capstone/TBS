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
using TBS.Data.Models.Posts.Carrier;

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
            var orderedPosts = allUserPosts.OrderByDescending(p => p.PostStatus);
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
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Carrier)
                    .ThenInclude(c => c.Reviews)
                .Include(p => p.Vehicle)
                .Include(p => p.PickupLocation)
                .Include(p => p.PickupContact)
                .Include(p => p.DropoffLocation)
                .Include(p => p.DropoffContact)
           
                .FirstOrDefaultAsync(p => p.Id == id);

            shipperPost.Bids = shipperPost.Bids.OrderByDescending(b => b.BidStatus);

            if (shipperPost == null)
            {
                throw new InvalidShipperPostException();
            }

            return shipperPost;
        }

        public async Task<PaginatedShipperPosts> SearchAllActivePostsAsync(SearchModel request, PaginationModel model)
        {
            var filteredPosts = await _context.ShipperPosts
                .Include(p => p.Vehicle)
                .Include(p => p.PickupLocation)
                .Include(p => p.DropoffLocation)
                .Where(p => p.PostStatus == PostStatus.Open)
                .Where(p => request.pickupDate == DateTime.Parse("10/10/1000") || DateTime.Compare(p.PickupDate, request.pickupDate) >= 0)
                .Where(p => request.dropOffDate == DateTime.Parse("10/10/1000") || DateTime.Compare(p.DropoffDate, request.dropOffDate) <= 0)
                .Where(p => request.pickupCity == "" || p.PickupLocation.City == request.pickupCity)
                .Where(p => request.dropoffCity == "" ||  p.DropoffLocation.City == request.dropoffCity)
                .ToListAsync();
            model.Count = filteredPosts.Count();
            Console.WriteLine(model.Count);
            var paginatedPosts = filteredPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();

            return new PaginatedShipperPosts() { PaginationModel = model, Posts = paginatedPosts };

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

            var bids = await _context.ShipperBids.Where(b => b.Post.Id == post.Id).ToListAsync();

            foreach (var bid in bids)
            {
                _context.ShipperBids.Remove(bid);
                _logger.LogInformation($"Shipper Post: Successfully automatically deleted a bid {bid.Id} for ${bid.BidAmount}. ({post.Id})");
            }

            _context.ShipperPosts.Remove(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Shipper Post: Successfully deleted a post {post.PickupLocation.City} -> {post.DropoffLocation.City}. ({post.Id})");

            return await Task.FromResult(true);
        }
    }
}