using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Models.Posts.Carrier;

namespace TBS.API.Controllers.v1.Posts
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Posts/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierPostService _service;

        public CarrierController(ICarrierPostService service)
        {
            _service = service;
        }

        // GET: api/v1/Posts/Carrier/{userFirebaseId}/{currentPage}/{count}
        [HttpGet("{userFirebaseId}/{currentPage}/{count}")]
        [Authorize]
        public async Task<IActionResult> GetCarriersPostsAsync(string userFirebaseId, int currentPage, int count) => Ok(new { result = await _service.GetAllUsersPostsAsync(userFirebaseId, new Data.Models.PaginationModel() { CurrentPage = currentPage, Count = count }) });

        // GET: api/v1/Posts/Carrier/{currentPage}/{pageSize}
        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<IActionResult> GetActiveCarriersPostsAsync(int currentPage, int pageSize) => Ok(new { result = await _service.GetAllActivePostsAsync(new Data.Models.PaginationModel() { CurrentPage = currentPage, PageSize = pageSize }) });

        // GET: api/v1/Posts/Carrier/{PostId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCarrierPostByIdAsync(int postId) => Ok(new { result = await _service.GetPostByIdAsync(postId) });

        // POST: api/v1/Posts/Carrier
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCarrierPostAsync(CarrierPost post)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreatePostAsync(id, post) });
        }

        // POST: api/v1/Posts/Carrier/{PostId}
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> PutCarrierUpdateAsync(int postId, CarrierPost post) => Ok(new { result = await _service.UpdatePostAsync(postId, post) });

        // DELETE: api/v1/Posts/Carrier/{PostId}
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCarrierPostAsync(int postId) => Ok(new { result = await _service.DeletePostAsync(postId) });
    }
}
