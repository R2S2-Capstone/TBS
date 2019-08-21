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
    public class ShipperController : ControllerBase
    {
        private readonly IShipperBidService _service;

        public ShipperController(IShipperBidService service)
        {
            _service = service;
        }

        // GET: api/v1/Bids/Shipper/{bidId}
        [HttpGet("{bidId}")]
        [Authorize]
        public async Task<IActionResult> GetBidByIdAsync(string bidId) => Ok(new { result = await _service.GetBidByIdAsync(Guid.Parse(bidId)) });

        // GET: api/v1/Bids/Shipper/{userFirebaseId}/{bidId}/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{postId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetAllBidsByPostIdAsync(string userFirebaseId, string postId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllBidsByPostIdAsync(userFirebaseId, Guid.Parse(postId), new PaginationModel { CurrentPage = currentPage, PageSize = pageSize }) });

        // GET: api/v1/Bids/Shipper/{userFirebaseId}/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersBidsAsync(string userFirebaseId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllUsersBidsAsync(userFirebaseId, new PaginationModel { CurrentPage = currentPage, PageSize = pageSize }) });

        // POST: api/v1/Bids/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostShipperBidAsync(ShipperCreateBidRequest request)
        {
            var userFirebaseId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreateBidAsync(userFirebaseId, request) });
        }

        // PUT: api/v1/Bids/Shipper/
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutUpdateShipperBidAsync(UpdateBidRequest request) => Ok(new { result = await _service.UpdateBidAsync(request) });
    }
}
