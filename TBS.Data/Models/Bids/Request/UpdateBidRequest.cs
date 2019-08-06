using System;

namespace TBS.Data.Models.Bids.Request
{
    public class UpdateBidRequest
    {
        public Guid BidId { get; set; }

        public BidStatus Status { get; set; }
    }
}
