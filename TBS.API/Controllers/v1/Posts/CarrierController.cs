using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models.Post.Carrier;

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

        // GET: api/v1/Posts/Carrier/{userFirebaseId}{currentPage}/{count}
        [HttpGet("{userFirebaseId}/{currentPage}/{count}")]
        [Authorize]
        public async Task<IActionResult> GetCarriersPosts(string userFirebaseId, int currentPage, int count) => Ok(new { result = await _service.GetAllUsersPosts(userFirebaseId, new Data.Models.PaginationModel() { CurrentPage = currentPage, Count = count }) });

        // GET: api/v1/Posts/Carrier/{currentPage}/{count}
        [HttpGet("{currentPage}/{count}")]
        public async Task<IActionResult> GetActiveCarriersPosts(int currentPage, int count) => Ok(new { result = await _service.GetAllActivePosts( new Data.Models.PaginationModel() { CurrentPage = currentPage, Count = count }) });

        // GET: api/v1/Posts/Carrier/{PostId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCarrierPost(int postId) => Ok(new { result = await _service.GetPostById(postId) });

        // POST: api/v1/Posts/Carrier
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCarrierPost(CarrierPost post)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreatePostAsync(id, post) });
        }

        // POST: api/v1/Posts/Carrier/{PostId}
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> PutCarrierPost(int postId, CarrierPost post) => Ok(new { result = await _service.UpdatePostAsync(postId, post) });

        // DELETE: api/v1/Posts/Carrier/{PostId}
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCarrierPost(int postId) => Ok(new { result = await _service.DeletePostAsync(postId) });
    }
}
