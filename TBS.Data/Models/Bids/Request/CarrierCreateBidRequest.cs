using System;
using TBS.Data.Models.Bids.Carrier;

namespace TBS.Data.Models.Bids.Request
{
    public class CarrierCreateBidRequest
    {
        public Guid PostId { get; set; }
        public CarrierBid Bid { get; set; }
    }
}
