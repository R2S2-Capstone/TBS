using System.Collections.Generic;
using TBS.Data.Models.Posts.Carrier;

namespace TBS.Data.Models.Posts.Response
{
    public class PaginatedCarrierPosts
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<CarrierPost> Posts { get; set; }
    }
}
