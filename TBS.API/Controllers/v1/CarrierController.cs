using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBS.Data.Interfaces.User;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Request;
using TBS.Data.Models.Post.Response;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _service;

        public CarrierController(ICarrierService service)
        {
            _service = service;
        }

        // GET: api/v1/Shipper/{id}/Posts/All
        [HttpGet("{id}/Posts/All")]
        [Authorize]
        public async Task<PaginatedPosts> GetCarriersPosts(GetAllUsersPostsRequest request) => await _service.GetAllUsersPosts(request);

        // TODO: Use HTTP Context ID (Firebase ID) and match it with the user associated with the post (Given the post ID we can get the post which has an association with a user which will contain the firebase ID)
        // GET: api/v1/Shipper/{id}/Posts/{PostId}
        [HttpGet("{id}/Posts/{postId}")]
        public async Task<CarrierPost> GetShipperPost(GetPostByIdRequest request) => await _service.GetPostById(request);
    }
}
