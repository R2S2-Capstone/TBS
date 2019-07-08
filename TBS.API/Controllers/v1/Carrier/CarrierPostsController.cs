using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TBS.Data.Interfaces.Post;
using TBS.Data.Models.Post;

namespace TBS.API.Controllers.v1.Posts
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Carrier/Posts")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierPostsController : ControllerBase
    {
        private readonly ICarrierPostService _service;

        public CarrierPostsController(ICarrierPostService service)
        {
            _service = service;
        }

        // GET: api/Carrier/Posts
        [HttpGet]
        public async Task<IEnumerable<CarrierPost>> GetCarrierPosts() => await _service.GetAllPostsAsync();

        // GET: api/Carrier/Posts/{id}
        [HttpGet("{id}")]
        public async Task<CarrierPost> GetCarrierPost(int id) => await _service.GetPostByIdAsync(id);

        // PUT: api/Carrier/Posts/{id}
        [HttpPut("{id}")]
        public async Task<bool> PutCarrierPost(int id, CarrierPost post) => await _service.UpdatePostAsync(id, post);

        // POST: api/Carrier/Posts
        [HttpPost]
        public async Task<bool> PostCarrierPost(CarrierPost post) => await _service.CreatePostAsync(post);

        // DELETE: api/Carrier/Posts/{id}
        [HttpDelete("{id}")]
        public async Task<bool> DeleteCarrierPost(int id) => await _service.RemovePostAsync(id);
    }
}
