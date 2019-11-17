using System.Collections.Generic;

namespace TBS.Data.Models.Reviews
{
    public class PaginatedShipperReviews
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<ShipperReview> ShipperReviews { get; set; }
    }
}
