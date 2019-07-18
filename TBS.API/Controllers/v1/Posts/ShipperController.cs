using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models.Post.Shipper;

namespace TBS.API.Controllers.v1.Posts
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Posts/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperPostService _service;

        public ShipperController(IShipperPostService service)
        {
            _service = service;
        }

        // GET: api/v1/Posts/Shipper/All/{currentPage}/{count}
        [HttpGet("{currentPage}/{count}")]
        [Authorize]
        public async Task<IActionResult> GetShippersPosts(int currentPage, int count)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.GetAllUsersPosts(id, new Data.Models.PaginationModel() { CurrentPage = currentPage, Count = count }) });
        }

        // GET: api/v1/Posts/Shipper/{PostId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetShipperPost(int postId) => Ok(new { result = await _service.GetPostById(postId) });

        // POST: api/v1/Posts/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostShipperPost(ShipperPost post)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreatePostAsync(id, post) });
        }

        // PUT: api/v1/Posts/Shipper
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutShipperPost(int postId, ShipperPost post) => Ok(new { result = await _service.UpdatePostAsync(postId, post) });

        // DELETE: api/v1/Posts/Shipper
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteShipperPost(int postId) => Ok(new { result = await _service.DeletePostAsync(postId) });
    }
}
