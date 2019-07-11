using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.User;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _service;

        public CarrierController(ICarrierService service)
        {
            _service = service;
        }

        // GET: api/v1/Carrier/Posts/All
        [HttpGet("{id}/Posts/All/{currentPage}/{count}")]
        public async Task<PaginatedPosts> GetCarriersPosts(int currentPage, int count)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return await _service.GetAllUsersPosts(id, new Data.Models.PaginationModel() { CurrentPage = currentPage, Count = count });
        }

        // GET: api/v1/Carrier/Posts/{PostId}
        [HttpGet("Posts/{postId}")]
        public async Task<CarrierPost> GetShipperPost(int postId) => await _service.GetPostById(postId);
    }
}
