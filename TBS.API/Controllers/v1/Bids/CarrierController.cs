using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Carrier;

namespace TBS.API.Controllers.v1.Bids
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Bids/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierBidService _service;

        public CarrierController(ICarrierBidService service)
        {
            _service = service;
        }

        // GET: api/v1/Bids/Carrier/{bidId}
        [HttpGet("{bidId}")]
        [Authorize]
        public async Task<IActionResult> GetBidById(int bidId) => Ok(new { result = await _service.GetBidById(bidId) });

        // GET: api/v1/Bids/Carrier/{userFirebaseId}/{bidId}/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{postId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetAllBidsByPostId(string userFirebaseId, int bidId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllBidsByPostId(userFirebaseId, bidId, new PaginationModel { CurrentPage = currentPage, PageSize = pageSize }) } );

        // GET: api/v1/Bids/Carrier/{userFirebaseId}/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersBids(string userFirebaseId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllUsersBids(userFirebaseId, new PaginationModel { CurrentPage = currentPage, PageSize = pageSize } ) });

        // POST: api/v1/Posts/Carrier
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCarrierBid(CarrierBid bid)
        {
            var userFirebaseId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreateBidAsync(userFirebaseId, bid) });
        }

        // DELETE: api/v1/Posts/Carrier/{bidId}
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCarrierBid(int bidId) => Ok(new { result = await _service.DeleteBidAsync(bidId) });
    }
}
