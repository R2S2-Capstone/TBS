using System;
using System.Collections.Generic;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class PaginatedCarrierReviews
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<CarrierReviews> CarrierReviews { get; set; }
    }
}
