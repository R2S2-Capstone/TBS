using System.Collections.Generic;
using TBS.Data.Models.Posts.Shipper;

namespace TBS.Data.Models.Posts.Response
{
    public class PaginatedShipperPosts
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<ShipperPost> Posts { get; set; }
    }
}
