﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TBS.Data.Database;
using TBS.Data.Interfaces.Reviews;
using TBS.Data.Models.Reviews;

namespace TBS.Services.Reviews
{
    public class ShipperReviewService : IShipperReviewService
    {
        private readonly DatabaseContext _context;

        public ShipperReviewService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<bool> CreateReviewAsync(ShipperCreateReviewRequest request)
        {
            var review = new ShipperReview
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

                review.Shipper = carrierBid.Shipper;
                review.Carrier = carrierBid.Post.Carrier;

                await _context.ShipperReviews.AddAsync(review);
                await _context.SaveChangesAsync();

                carrierBid.ShipperReview = review;
                _context.CarrierBids.Update(carrierBid);

                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            else
            {
                var shipperBid = await _context.ShipperBids
                    .Include(b => b.Carrier)
                    .Include(b => b.Post.Shipper)
                    .Include(b => b.ShipperReview)
                    .FirstOrDefaultAsync(b => b.Id == request.BidId);

                review.Shipper = shipperBid.Post.Shipper;
                review.Carrier = shipperBid.Carrier;

                await _context.ShipperReviews.AddAsync(review);
                await _context.SaveChangesAsync();

                shipperBid.ShipperReview = review;
                _context.ShipperBids.Update(shipperBid);

                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }
    }
}
