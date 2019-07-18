using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models;
using TBS.Data.Models.Post.Response;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }

        // GET: api/v1/Posts/Carrier/
        [HttpGet("Carrier")]
        public async Task<PaginatedCarrierPosts> GetCarrierPosts(PaginationModel model) => await _service.GetAllActiveCarrierPosts(model);

        // GET: api/v1/Posts/Shipper
        [HttpGet("Shipper")]
        public async Task<PaginatedShipperPosts> GetShipperPosts(PaginationModel model) => await _service.GetAllActiveShipperPosts(model);
    }
}
