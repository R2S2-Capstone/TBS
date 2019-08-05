using TBS.Data.Models.Bids.Shipper;

namespace TBS.Data.Models.Bids.Request
{
    public class ShipperCreateBidRequest
    {
        public int PostId { get; set; }
        public ShipperBid Bid { get; set; }
    }
}
