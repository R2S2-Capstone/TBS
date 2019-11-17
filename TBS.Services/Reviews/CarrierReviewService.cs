using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Interfaces.Reviews;
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

        public async Task<bool> CreateReviewAsync(CarrierCreateReviewRequest request)
        {
            var review = new CarrierReview
            {
                Rating = request.Rating,
                Review = request.Review,
                ReviewDate = DateTime.Now
            };

            if (request.BidBoolean)
            {
                var carrierBid = await _context.CarrierBids
                    .Include(b => b.Shipper)
                    .Include(b => b.Post.Carrier)
                    .Include(b => b.CarrierReview)
                    .FirstOrDefaultAsync(b => b.Id == request.BidId);

                review.Carrier = carrierBid.Post.Carrier;
                review.Shipper = carrierBid.Shipper;

                await _context.CarrierReviews.AddAsync(review);
                await _context.SaveChangesAsync();

                carrierBid.CarrierReview = review;
                _context.CarrierBids.Update(carrierBid);

                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            else
            {
                var shipperBid = await _context.ShipperBids
                        .Include(b => b.Carrier)
                        .Include(b => b.Post.Shipper)
                        .Include(b => b.CarrierReview)
                        .FirstOrDefaultAsync(b => b.Id == request.BidId);

                review.Carrier = shipperBid.Carrier;
                review.Shipper = shipperBid.Post.Shipper;

                await _context.CarrierReviews.AddAsync(review);
                await _context.SaveChangesAsync();

                shipperBid.CarrierReview = review;
                _context.ShipperBids.Update(shipperBid);

                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }
    }
}
