using System;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Data.Models.Bids.Request
{
    public class ShipperCreateBidRequest
    {
        public Guid PostId { get; set; }
        public ShipperBid Bid { get; set; }
    }
}
