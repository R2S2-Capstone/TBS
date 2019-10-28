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


        public async Task<Array> GetAllReviewsByCarrierIdAsync(Guid carrierId)
        {

            var reviews = await Task.FromResult(
               _context.CarrierReview
               .Include(b => b.rating)
               .Include(b => b.review)
               .Include(b => b.ID)
               .Where(b => b.Carrier.Id == carrierId));
            var ArrayReviews = reviews.ToArray();
            return ArrayReviews;

        }
        public async Task<bool> CreateReviewAsync(CarrierCreateReviewRequest request)
        {
            if (request.isCarrierBid)
            {
                CarrierBid bid = await Task.FromResult(
                     _context.CarrierBids
                    .Include(b => b.Shipper)
                    .Include(b => b.Post)
                    .Include(b => b.carrierReview)
                    .FirstOrDefault(b => b.Id == request.bidId));
                request.review.date = DateTime.Now;
                await _context.CarrierReview.AddAsync(request.review);
                await _context.SaveChangesAsync();
                bid.carrierReview = await _context.CarrierReview
                    .Include(b => b.rating)
                    .Include(b => b.review)
                    .Include(b => b.Carrier)
                    .Include(b => b.ID)
                    .FirstOrDefaultAsync(b => b.review == request.review.review && b.date == request.review.date);
                _context.CarrierBids.Update(bid);
                return await Task.FromResult(true);
            }

            ShipperBid secondaryBid = await Task.FromResult(
                     _context.ShipperBids
                    .Include(b => b.Carrier)
                    .Include(b => b.Post)
                    .Include(b => b.carrierReview)
                    .FirstOrDefault(b => b.Id == request.bidId));
            request.review.date = DateTime.Now;
            await _context.CarrierReview.AddAsync(request.review);
            await _context.SaveChangesAsync();
            secondaryBid.carrierReview = await _context.CarrierReview
                .Include(b => b.rating)
                .Include(b => b.review)
                .Include(b => b.Carrier)
                .Include(b => b.ID)
                .FirstOrDefaultAsync(b => b.review == request.review.review && b.date == request.review.date);
            _context.ShipperBids.Update(secondaryBid);
            return await Task.FromResult(true);
        }
    }
}
