using System;
using System.Collections.Generic;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class CarrierCreateReviewRequest 
    {
        public Guid bidId { get; set; }
        public CarrierReviews review { get; set; }
        public bool isCarrierBid { get; set; }
    }
}
