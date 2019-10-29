using System;
using System.Collections.Generic;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class ShipperCreateReviewRequest
    {
        public Guid bidId { get; set; }
        public ShipperReview review { get; set; }
        public bool bidBoolean { get; set; }
    }
}
