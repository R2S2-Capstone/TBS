using System;
using System.Collections.Generic;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class PaginatedShipperReviews
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<ShipperReview> ShipperReviews{ get; set; }
    }
}
