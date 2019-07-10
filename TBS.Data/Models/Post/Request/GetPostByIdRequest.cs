namespace TBS.Data.Models.Post.Request
{
    public class GetPostByIdRequest
    {
        public string UserFirebaseId { get; set; }
        public int PostId { get; set; }
    }
}
