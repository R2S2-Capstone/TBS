namespace TBS.Data.Models.Post.Request
{
    public class GetAllUsersPostsRequest
    {
        public int UserId { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
