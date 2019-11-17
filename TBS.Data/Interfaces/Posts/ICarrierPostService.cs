using System;
using System.Threading.Tasks;
using TBS.Data.Models;
using TBS.Data.Models.Posts.Carrier;
using TBS.Data.Models.Posts.Request;
using TBS.Data.Models.Posts.Response;

namespace TBS.Data.Interfaces.Posts
{
    public interface ICarrierPostService
    {
        Task<PaginatedCarrierPosts> GetAllActivePostsAsync(PaginationModel model);

        Task<PaginatedCarrierPosts> GetAllUsersPostsAsync(string userFirebaseId, PaginationModel model);

        Task<CarrierPost> GetPostByIdAsync(Guid postId);

        Task<PaginatedCarrierPosts> GetSearchAllActivePostsAsync(SearchModel request, PaginationModel model);

        Task<bool> CreatePostAsync(string userFirebaseId, CarrierPost post);

        Task<bool> UpdatePostAsync(Guid id, CarrierPost post);

        Task<bool> DeletePostAsync(Guid id);
    }
}
