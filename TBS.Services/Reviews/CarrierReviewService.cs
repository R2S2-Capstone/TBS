using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Interfaces.Reviews;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;
using TBS.Data.Models.Bids.Shipper;
using TBS.Data.Models.Reviews;

namespace TBS.Services.Reviews
{
    public class CarrierReviewService: ICarrierReviewService
    {
        private readonly DatabaseContext _context;

        public CarrierReviewService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<CarrierReviews> GetReviewByIdAsync(Guid reviewId)
        {
            return await Task.FromResult(
                _context.CarrierReview
                .FirstOrDefault(b => b.ID == reviewId)
            );
        }
        public async Task<Array> GetAllReviewsByCarrierIdAsync(Guid carrierId)
        {

            var reviews = await Task.FromResult(
               _context.CarrierReview
               .Include(b => b.Carrier)
               .Where(b => b.Carrier.Id == carrierId));
            var ArrayReviews = reviews.ToArray();
            return ArrayReviews;

        }
        public async Task<bool> CreateReviewAsync(CarrierCreateReviewRequest request)
        {
            CarrierReviews createdReview = new CarrierReviews();
            createdReview.rating = request.rating;
            createdReview.review = request.review;
            createdReview.date = DateTime.Now;
            if (request.bidBoolean)
            {
                CarrierBid bid = await Task.FromResult(
                     _context.CarrierBids
                    .Include(b => b.Shipper)
                    .Include(b => b.Post)
                    .Include(b => b.carrierReview)
                    .FirstOrDefault(b => b.Id == request.bidId));
                createdReview.Carrier = bid.Post.Carrier;
                createdReview.reviewer = bid.Shipper.Name;
                await _context.CarrierReview.AddAsync(createdReview);
                await _context.SaveChangesAsync();
                bid.carrierReview = createdReview;
                _context.CarrierBids.Update(bid);
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }

            ShipperBid secondaryBid = await Task.FromResult(
                     _context.ShipperBids
                    .Include(b => b.Carrier)
                    .Include(b => b.Post.Shipper)
                    .Include(b => b.carrierReview)
                    .FirstOrDefault(b => b.Id == request.bidId));
            createdReview.Carrier = secondaryBid.Carrier;
            createdReview.reviewer = secondaryBid.Post.Shipper.Name;
            await _context.CarrierReview.AddAsync(createdReview);
            await _context.SaveChangesAsync();
            secondaryBid.carrierReview = createdReview;
            _context.ShipperBids.Update(secondaryBid);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
