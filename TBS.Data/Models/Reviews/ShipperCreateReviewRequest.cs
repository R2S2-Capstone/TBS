using System;
using System.Collections.Generic;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class ShipperCreateReviewRequest
    {
        public Guid bidId { get; set; }
        public String review { get; set; }
        public int rating { get; set; }
        public bool bidBoolean { get; set; }
    }
}
