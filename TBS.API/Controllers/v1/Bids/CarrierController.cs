using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Models;
using TBS.Data.Models.Bids.Request;

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
        public async Task<IActionResult> GetBidByIdAsync(string bidId) => Ok(new { result = await _service.GetBidByIdAsync(Guid.Parse(bidId)) });

        // GET: api/v1/Bids/Carrier/{userFirebaseId}/{bidId}/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{postId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetAllBidsByPostIdAsync(string userFirebaseId, string postId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllBidsByPostIdAsync(userFirebaseId, Guid.Parse(postId), new PaginationModel { CurrentPage = currentPage, PageSize = pageSize }) } );

        // GET: api/v1/Bids/Carrier/{userFirebaseId}/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersBidsAsync(string userFirebaseId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllUsersBidsAsync(userFirebaseId, new PaginationModel { CurrentPage = currentPage, PageSize = pageSize } ) });

        // POST: api/v1/Posts/Carrier
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCarrierBidAsync(CarrierCreateBidRequest request)
        {
            var userFirebaseId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreateBidAsync(userFirebaseId, request) });
        }

        // PUT: api/v1/Bids/Carrier/
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PostUpdateCarrierBidAsync(UpdateBidRequest request) => Ok(new { result = await _service.UpdateBidAsync(request) });

        // DELETE: api/v1/Posts/Carrier/{bidId}
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCarrierBidAsync(string bidId) => Ok(new { result = await _service.DeleteBidAsync(Guid.Parse(bidId)) });
    }
}
