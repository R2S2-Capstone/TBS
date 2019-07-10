using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Post.Shipper;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.API.Controllers.v1.Shipper
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Shipper/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IShipperPostService _service;

        public PostsController(IShipperPostService service)
        {
            _service = service;
        }
        //Task<PaginatedPosts> GetAllPosts(GetAllUsersPostsRequest request);

        //Task<ShipperPost> GetPostById(int id);

        //Task<bool> CreateShipperPostAsync(ShipperPost post);

        //Task<bool> UpdateShipperPostAsync(int id, ShipperPost post);

        //Task<bool> RemoveShipperPostAsync(int id);

        // GET: api/v1/Shipper/Posts
        [HttpGet]
        public async Task<PaginatedPosts> GetShipperPosts(PaginationModel model) => await _service.GetAllActivePosts(model);

        // GET: api/v1/Shipper/Posts/{id}
        [HttpGet("{id}")]
        public async Task<ShipperPost> GetShipperPost(int id) => await _service.GetPostByIdAsync(id);
    }
}
