using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Post.Shipper;
using TBS.Data.Models.Post.Shipper;

namespace TBS.API.Controllers.v1.Shipper
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Shipper/Posts")]
    [Produces("application/json")]
    [ApiController]
    public class ShipperPostsController : ControllerBase
    {
        private readonly IShipperPostService _service;

        public ShipperPostsController(IShipperPostService service)
        {
            _service = service;
        }

        // GET: api/Shipper/Posts
        [HttpGet]
        public async Task<IEnumerable<ShipperPost>> GetShipperPosts() => await _service.GetAllPostsAsync();

        // GET: api/Shipper/Posts/{id}
        [HttpGet("{id}")]
        public async Task<ShipperPost> GetShipperPost(int id) => await _service.GetPostByIdAsync(id);

        // PUT: api/Shipper/Posts/{id}
        [HttpPut("{id}")]
        public async Task<bool> PutShipperPost(int id, ShipperPost post) => await _service.UpdatePostAsync(id, post);

        // POST: api/Shipper/Posts
        [HttpPost]
        public async Task<bool> PostShipperPost(ShipperPost post) => await _service.CreatePostAsync(post);

        // DELETE: api/Shipper/Posts/{id}
        [HttpDelete("{id}")]
        public async Task<bool> DeleteShipperPost(int id) => await _service.RemovePostAsync(id);
    }
}
