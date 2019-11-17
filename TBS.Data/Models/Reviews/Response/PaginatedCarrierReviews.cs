using System.Collections.Generic;

namespace TBS.Data.Models.Reviews
{
    public class PaginatedCarrierReviews
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<CarrierReview> CarrierReviews { get; set; }
    }
}
