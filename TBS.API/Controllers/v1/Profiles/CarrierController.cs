using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Data.Interfaces.Profiles;
using TBS.Data.Models.Users;

namespace TBS.API.Controllers.v1.Profiles
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Profiles/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierProfileService _service;

        public CarrierController(ICarrierProfileService service)
        {
            _service = service;
        }

        // GET: api/v1/Profiles/Carrier
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyCarrierProfilesAsync()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.GetMyProfileAsync(id) });
        }

        // GET: api/v1/Profiles/Carrier/{profileId}
        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetCarriersProfileAsync(string profileId) => Ok(new { result = await _service.GetProfileByIdAsync(Guid.Parse(profileId))});

        // PATCH: api/v1/Profiles/Carrier
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchCarrierUpdateAsync(Carrier carrier)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.UpdateProfileAsync(id, carrier) });
        }
    }
}