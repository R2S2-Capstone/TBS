using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Interfaces;
using TBS.Data.Interfaces.Reviews;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Request;
using TBS.Data.Models.Bids.Shipper;
using TBS.Data.Models.Reviews;
using TBS.Services.Bids;

namespace TBS.Services.Reviews
{
    public class ShipperReviewService : IShipperReviewService
    {

        private readonly DatabaseContext _context;

        public ShipperReviewService(DatabaseContext databaseContext) {
            _context = databaseContext;
        }


        public async Task<ShipperReview> GetReviewByIdAsync(Guid reviewId)
        {
            Console.WriteLine(reviewId);
            return await Task.FromResult(
                _context.ShipperReviews
                .FirstOrDefault(b => b.ID == reviewId)
            );
        }
        public async Task<Array> GetAllReviewsByShipperIdAsync(Guid shipperId) {
            Console.WriteLine(shipperId.ToString());
            var reviews = await Task.FromResult(
                _context.ShipperReviews
                .Include(b => b.shipper)
                .Where(b => b.shipper.Id == shipperId));
           
            var ArrayReviews = reviews.ToArray<ShipperReview>();
            Console.WriteLine(ArrayReviews[0].review);
            return ArrayReviews;

        }
        public async Task<bool> CreateReviewAsync(ShipperCreateReviewRequest request) {

            ShipperReview createdReview = new ShipperReview();
            createdReview.rating = request.rating;
            createdReview.review = request.review;
            createdReview.date = DateTime.Now;
            Console.WriteLine(request.bidId.ToString());
            if (request.bidBoolean) {
                CarrierBid x = await Task.FromResult(
                _context.CarrierBids
               .Include(b => b.Shipper)
               .Include(b => b.Post.Carrier)
               .Include(b => b.carrierReview)
               .FirstOrDefault(b => b.Id == request.bidId));
                createdReview.shipper = x.Shipper;
                Console.WriteLine(x.BidStatus.ToString());
                createdReview.reviewer = x.Post.Carrier.Name;
                await _context.ShipperReviews.AddAsync(createdReview);
                await _context.SaveChangesAsync();
                x.shipperReview = createdReview;
                _context.CarrierBids.Update(x);
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }

            ShipperBid bid = await Task.FromResult(
                 _context.ShipperBids
                .Include(b => b.Carrier)
                .Include(b => b.Post)
                .Include(b => b.shipperReview)
                .FirstOrDefault(b => b.Id == request.bidId));
            createdReview.shipper = bid.Post.Shipper;
            createdReview.reviewer = bid.Carrier.Name;
            await _context.ShipperReviews.AddAsync(createdReview);
            await _context.SaveChangesAsync();
            bid.shipperReview = createdReview;
            _context.ShipperBids.Update(bid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
