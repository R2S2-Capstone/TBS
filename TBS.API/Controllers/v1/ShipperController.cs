using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBS.Data.Interfaces.User;
using TBS.Data.Models.Post.Request;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _service;

        public ShipperController(IShipperService service)
        {
            _service = service;
        }

        // GET: api/v1/Shipper/{id}/Posts/All
        [HttpGet("{id}/Posts/All")]
        public async Task<PaginatedPosts> GetShippersPosts(GetAllUsersPostsRequest model) => await _service.GetAllUsersPosts(model);


        // TODO: Use HTTP Context ID (Firebase ID) and match it with the user associated with the post (Given the post ID we can get the post which has an association with a user which will contain the firebase ID
        //var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        // GET: api/v1/Shipper/{id}/Posts/{PostId}
        [HttpGet("{id}/Posts/{postId}")]
        public async Task<ShipperPost> GetShipperPost(GetPostByIdRequest request) => await _service.GetPostById(request);
    }
}
