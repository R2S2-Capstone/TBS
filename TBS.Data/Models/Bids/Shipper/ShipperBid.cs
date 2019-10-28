using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Posts.Shipper;
using TBS.Data.Models.Reviews;

namespace TBS.Data.Models.Bids.Shipper
{
    // Note: this is a bid placed on a shippers post by a carrier
    public class ShipperBid
    {
        public Guid Id { get; set; }

        public Users.Carrier Carrier { get; set; }

        public ShipperPost Post { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double BidAmount { get; set; }

        public DateTime DateBidPlaced { get; set; } = DateTime.Now;

        public BidStatus BidStatus { get; set; } = BidStatus.Open;

        public Reviews.CarrierReviews carrierReview { get; set; }

        public Reviews.ShipperReview shipperReview { get; set; }

        public static implicit operator ShipperBid(ShipperReview v)
        {
            throw new NotImplementedException();
        }
    }
}
