using System;

namespace TBS.Data.Models.Reviews
{
    public class ShipperCreateReviewRequest
    {
        public Guid BidId { get; set; }

        public string Review { get; set; }

        public double Rating { get; set; }

        public bool BidBoolean { get; set; }
    }
}
