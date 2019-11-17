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

        // POST: api/v1/Reviews/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostShipperReviewAsync(ShipperCreateReviewRequest review) => Ok(new { result = await _service.CreateReviewAsync(review) });
    }
}