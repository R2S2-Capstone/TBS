﻿namespace TBS.Data.Models.Post.Request
{
    public class GetAllUsersPostsRequest
    {
        public string UserFirebaseId { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}