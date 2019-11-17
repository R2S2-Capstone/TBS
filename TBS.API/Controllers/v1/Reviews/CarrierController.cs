using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Reviews;
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

        // POST: api/v1/Reviews/Carrier
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCarrierReviewAsync(CarrierCreateReviewRequest review) => Ok(new { result = await _service.CreateReviewAsync(review) });
    }
}
