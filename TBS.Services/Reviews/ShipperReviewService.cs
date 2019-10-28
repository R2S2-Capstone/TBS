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
    class ShipperReviewService : IShipperReviewService
    {

        private readonly DatabaseContext _context;

        public ShipperReviewService(DatabaseContext databaseContext) {
            _context = databaseContext;
        }


        public async Task<Array> GetAllReviewsByShipperIdAsync(Guid shipperId) {

             var reviews = await Task.FromResult(
                _context.ShipperReviews
                .Include(b => b.rating)
                .Include(b => b.review)
                .Include(b => b.ID)
                .Where(b => b.shipper.Id == shipperId));

            var ArrayReviews = reviews.ToArray<ShipperReview>();
            return ArrayReviews;

        }
        public async Task<bool> CreateReviewAsync(ShipperCreateReviewRequest request) {

            if (!request.isShipperBid) {
                CarrierBid x = await Task.FromResult(
                _context.CarrierBids
               .Include(b => b.Shipper)
               .Include(b => b.Post)
               .Include(b => b.carrierReview)
               .FirstOrDefault(b => b.Id == request.bidId));
                request.review.date = DateTime.Now;
                await _context.ShipperReviews.AddAsync(request.review);
                await _context.SaveChangesAsync();
                x.shipperReview = await _context.ShipperReviews
                    .Include(b => b.rating)
                    .Include(b => b.review)
                    .Include(b => b.ID)
                    .Include(b => b.shipper)
                    .FirstOrDefaultAsync(b => b.review == request.review.review && b.date == request.review.date);
                _context.CarrierBids.Update(x);
                return await Task.FromResult(true);
            }

            ShipperBid bid = await Task.FromResult(
                 _context.ShipperBids
                .Include(b => b.Carrier)
                .Include(b => b.Post)
                .Include(b => b.shipperReview)
                .FirstOrDefault(b => b.Id == request.bidId));
            request.review.date = DateTime.Now;
            await _context.ShipperReviews.AddAsync(request.review);
            await _context.SaveChangesAsync();
            bid.shipperReview = await _context.ShipperReviews
                .Include(b => b.rating)
                .Include(b => b.review)
                .Include(b => b.ID)
                .Include(b => b.shipper)
                .FirstOrDefaultAsync(b => b.review == request.review.review && b.date == request.review.date);
            _context.ShipperBids.Update(bid);
            return await Task.FromResult(true);
        }
    }
}
