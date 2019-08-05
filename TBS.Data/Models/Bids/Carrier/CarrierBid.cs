using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Posts.Carrier;

namespace TBS.Data.Models.Bids.Carrier
{
    // Note: this is a bid placed on a carriers post by a shipper
    public class CarrierBid
    {
        public int Id { get; set; }

        public Users.Shipper Shipper { get; set; }

        public CarrierPost Post { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double BidAmount { get; set; }

        public DateTime DateBidPlaced { get; set; }

        public BidStatus BidStatus { get; set; } = BidStatus.Open;
    }
}
