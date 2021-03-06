﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Exceptions.Posts;
using TBS.Data.Exceptions.Posts.Carrier;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Models;
using TBS.Data.Models.Posts;
using TBS.Data.Models.Posts.Carrier;
using TBS.Data.Models.Posts.Request;
using TBS.Data.Models.Posts.Response;

namespace TBS.Services.Posts
{
    public class CarrierPostService : ICarrierPostService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<CarrierPostService> _logger;

        public CarrierPostService(DatabaseContext databaseContext, ILogger<CarrierPostService> logger)
        {
            _context = databaseContext;
            _logger = logger;
        }

        public async Task<PaginatedCarrierPosts> GetAllActivePostsAsync(PaginationModel model)
        {
            var allPosts = await _context.CarrierPosts
                .Where(p => p.PostStatus == PostStatus.Open)
                .ToListAsync();

            model.Count = allPosts.Count();

            var paginatedPosts = allPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();

            return new PaginatedCarrierPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<PaginatedCarrierPosts> GetAllUsersPostsAsync(string userFirebaseId, PaginationModel model)
        {
            var user = await _context.Carriers
                .Include(c => c.Posts)
                .FirstOrDefaultAsync(c => c.UserFirebaseId == userFirebaseId);

            var allUserPosts = user.Posts.ToList();
            var orderedPosts = allUserPosts.OrderByDescending(p => p.PostStatus);

            model.Count = orderedPosts.Count();

            var paginatedPosts = orderedPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToList();

            return await Task.FromResult(new PaginatedCarrierPosts() { PaginationModel = model, Posts = paginatedPosts });
        }

        public async Task<CarrierPost> GetPostByIdAsync(Guid id)
        {
            var carrierPost = await _context.CarrierPosts
                .Include(p => p.Carrier)
                .Include(p => p.Carrier.Company)
                .Include(p => p.Carrier.Company.Contact)
                .Include(p => p.Carrier.Reviews)
                .Include(p => p.Carrier.Vehicle)
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Shipper)
                    .ThenInclude(b => b.Reviews)
                .Include(p => p.Bids)
                    .ThenInclude(b => b.CarrierReview)
                        .ThenInclude(r => r.Carrier)
                .Include(p => p.Bids)
                    .ThenInclude(b => b.ShipperReview)
                        .ThenInclude(r => r.Shipper)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (carrierPost == null)
            {
                throw new InvalidCarrierPostException();
            }

            carrierPost.Bids = carrierPost.Bids.OrderByDescending(b => b.BidStatus);

            return carrierPost;
        }

        public async Task<PaginatedCarrierPosts> GetSearchAllActivePostsAsync(SearchModel request, PaginationModel model)
        {
            var filteredPosts = await _context.CarrierPosts
                .Where(p => p.PostStatus == PostStatus.Open)
                .Where(p => request.DropoffDate == DateTime.Parse("10/10/1000") || p.PickupDate.DayOfYear >= request.PickupDate.DayOfYear)
                .Where(p => request.DropoffDate == DateTime.Parse("10/10/1000") || p.DropoffDate.DayOfYear <= request.DropoffDate.DayOfYear)
                .Where(p => request.PickupCity == "" || (p.PickupLocation.ToLower().Contains(request.PickupCity.ToLower())))
                .Where(p => request.DropoffCity == "" || p.DropoffLocation.ToLower().Contains(request.DropoffCity.ToLower()))
                .ToListAsync();

            model.Count = filteredPosts.Count();

            var paginatedPosts = filteredPosts
                .Skip((model.CurrentPage - 1) * model.PageSize)
                .Take(model.PageSize).ToArray();

            return new PaginatedCarrierPosts() { PaginationModel = model, Posts = paginatedPosts };
        }

        public async Task<bool> CreatePostAsync(string userFirebaseId, CarrierPost post)
        {
            post.Carrier = _context.Carriers.FirstOrDefault(s => s.UserFirebaseId == userFirebaseId);

            await _context.CarrierPosts.AddAsync(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Carrier Post: Successfully created a post {post.PickupLocation} -> {post.DropoffLocation}");

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdatePostAsync(Guid id, CarrierPost post)
        {
            if (id != post.Id)
            {
                throw new InvalidCarrierPostException();
            }

            post.UpdatedOn = DateTime.Now;
            _context.CarrierPosts.Update(post);

            _logger.LogInformation($"Carrier Post: Successfully updated a post {post.PickupLocation} -> {post.DropoffLocation} ({post.Id})");

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
                throw new InvalidCarrierPostException();
            }

            var bids = await _context.CarrierBids.Where(b => b.Post.Id == post.Id).ToListAsync();

            foreach (var bid in bids)
            {
                _context.CarrierBids.Remove(bid);
                _logger.LogInformation($"Carrier Post: Successfully automatically deleted a bid {bid.Id} for ${bid.BidAmount}. ({post.Id})");
            }

            _context.CarrierPosts.Remove(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Carrier Post: Successfully deleted a post {post.PickupLocation} -> {post.DropoffLocation}. ({post.Id})");

            return await Task.FromResult(true);
        }
    }
}
