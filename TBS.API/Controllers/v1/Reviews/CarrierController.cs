using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Interfaces.Reviews;
using TBS.Data.Models.Posts.Carrier;
using TBS.Data.Models.Reviews;

namespace TBS.API.Controllers.v1.Reviews
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Reviews/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierReviewService _service;

        public CarrierController(ICarrierReviewService service)
        {
            _service = service;
        }

        [HttpGet("{reviewId}/{trigger}")]
        public async Task<IActionResult> GetReviewByIdAsync(Guid reviewId) => Ok(new { result = await _service.GetReviewByIdAsync(reviewId) });
        // GET: api/v1/Reviews/Carrier/{carrier}/
        [HttpGet("{carrier}")]
        public async Task<IActionResult> GetReviewsAsync(Guid carrier) => Ok(new { result = await _service.GetAllReviewsByCarrierIdAsync(carrier) });

        // POST: api/v1/Reviews/Carrier
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCarrierReviewAsync(CarrierCreateReviewRequest review)
        {
            return Ok(new { result = await _service.CreateReviewAsync(review) });
        }      
    }
}
