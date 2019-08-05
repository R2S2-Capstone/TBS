using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Posts.Shipper;

namespace TBS.Data.Models.Bids.Shipper
{
    // Note: this is a bid placed on a shippers post by a carrier
    public class ShipperBid
    {
        public int Id { get; set; }

        public Users.Carrier Carrier { get; set; }

        public ShipperPost Post { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double BidAmount { get; set; }

        public DateTime DateBidPlaced { get; set; }

        public BidStatus BidStatus { get; set; } = BidStatus.Open;
    }
}
