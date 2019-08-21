using System.Collections.Generic;
using TBS.Data.Models.Bids.Carrier;

namespace TBS.Data.Models.Bids.Response
{
    public class PaginatedCarrierBids
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<CarrierBid> Bids { get; set; }
    }
}
