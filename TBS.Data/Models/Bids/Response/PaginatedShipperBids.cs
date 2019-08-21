using System.Collections.Generic;
using TBS.Data.Models.Bids.Shipper;

namespace TBS.Data.Models.Bids.Response
{
    public class PaginatedShipperBids
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<ShipperBid> Bids { get; set; }
    }
}
