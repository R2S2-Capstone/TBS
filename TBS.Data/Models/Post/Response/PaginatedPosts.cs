using System.Collections.Generic;

namespace TBS.Data.Models.Post.Response
{
    public class PaginatedPosts
    {
        public PaginationModel PaginationModel { get; set; }

        public IEnumerable<object> Posts { get; set; }
    }
}
