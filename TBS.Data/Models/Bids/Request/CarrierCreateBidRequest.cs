using TBS.Data.Models.Bids.Carrier;

namespace TBS.Data.Models.Bids.Request
{
    public class CarrierCreateBidRequest
    {
        public int PostId { get; set; }
        public CarrierBid Bid { get; set; }
    }
}
