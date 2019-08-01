using System.Collections.Generic;
using TBS.Data.Models.Post.Shipper;

namespace TBS.Data.Models.Post.Response
{
    public class PaginatedShipperPosts
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<ShipperPost> Posts { get; set; }
    }
}
