namespace TBS.Data.Models.Bids.Request
{
    public class UpdateBidRequest
    {
        public int BidId { get; set; }

        public BidStatus Status { get; set; }
    }
}
