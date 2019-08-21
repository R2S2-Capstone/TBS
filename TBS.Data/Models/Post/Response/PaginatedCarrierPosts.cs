using System.Collections.Generic;
using TBS.Data.Models.Post.Carrier;

namespace TBS.Data.Models.Post.Response
{
    public class PaginatedCarrierPosts
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<CarrierPost> Posts { get; set; }
    }
}
