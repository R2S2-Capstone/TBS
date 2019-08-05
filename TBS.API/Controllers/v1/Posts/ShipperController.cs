using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Models;
using TBS.Data.Models.Posts.Shipper;

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

        // GET: api/v1/Posts/Shipper/{currentPage}/{count}
        [HttpGet("{userFirebaseId}/{currentPage}/{count}")]
        [Authorize]
        public async Task<IActionResult> GetShippersPostsAsync(string userFirebaseId, int currentPage, int count) => Ok(new { result = await _service.GetAllUsersPostsAsync(userFirebaseId, new PaginationModel() { CurrentPage = currentPage, Count = count }) });

        // GET: api/v1/Posts/Shipper/{currentPage}/{pageSize}
        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<IActionResult> GetActiveShippersPostsAsync(int currentPage, int pageSize) => Ok(new { result = await _service.GetAllActivePostsAsync(new PaginationModel() { CurrentPage = currentPage, PageSize = pageSize }) });

        // GET: api/v1/Posts/Shipper/{PostId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetShipperPostAsync(int postId) => Ok(new { result = await _service.GetPostByIdAsync(postId) });

        // POST: api/v1/Posts/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostShipperPostAsync(ShipperPost post)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreatePostAsync(id, post) });
        }

        // POST: api/v1/Posts/Shipper/{PostId}
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> PutShipperPostAsync(int postId, ShipperPost post) => Ok(new { result = await _service.UpdatePostAsync(postId, post) });

        // DELETE: api/v1/Posts/Shipper/{PostId}
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteShipperPostAsync(int postId) => Ok(new { result = await _service.DeletePostAsync(postId) });
    }
}
