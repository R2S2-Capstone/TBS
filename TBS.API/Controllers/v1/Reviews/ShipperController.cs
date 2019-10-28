using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBS.Data.Interfaces.Reviews;
using TBS.Data.Models.Reviews;

namespace TBS.API.Controllers.v1.Reviews
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Reviews/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShipperController : Controller
    {
        private readonly IShipperReviewService _service;

        public ShipperController(IShipperReviewService service)
        {
            _service = service;
        }

        // GET: api/v1/Reviews/Shipper/
        [HttpGet("{shipper}")]
        public async Task<IActionResult> GetReviewsAsync(Guid shipper) => Ok(new { result = await _service.GetAllReviewsByShipperIdAsync(shipper) });

        // POST: api/v1/Reviews/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostShipperReviewAsync(ShipperCreateReviewRequest review)
        {
            return Ok(new { result = await _service.CreateReviewAsync(review) });
        }
    }
}