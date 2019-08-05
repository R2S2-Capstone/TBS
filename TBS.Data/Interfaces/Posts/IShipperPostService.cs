﻿using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Posts.Response;
using TBS.Data.Models.Posts.Shipper;

namespace TBS.Data.Interfaces.Posts
{
    public interface IShipperPostService
    {
        Task<PaginatedShipperPosts> GetAllActivePostsAsync(PaginationModel model);

        Task<PaginatedShipperPosts> GetAllUsersPostsAsync(string userFirebaseId, PaginationModel model);

        Task<ShipperPost> GetPostByIdAsync(int id);

        Task<bool> CreatePostAsync(string userFirebaseId, ShipperPost post);

        Task<bool> UpdatePostAsync(int id, ShipperPost post);

        Task<bool> DeletePostAsync(int id);
    }
}
