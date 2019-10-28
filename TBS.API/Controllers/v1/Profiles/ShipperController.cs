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
    public class ShipperController : ControllerBase
    {
        private readonly IShipperProfileService _service;

        public ShipperController(IShipperProfileService service)
        {
            _service = service;
        }

        // GET: api/v1/Profiles/Shipper
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyShipperProfilesAsync()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.GetMyProfileAsync(id) });
        }

        // GET: api/v1/Profiles/Shipper/{profileId}
        [HttpGet("{profileId}")]
        public async Task<IActionResult> GetShippersProfileAsync(string profileId) => Ok(new { result = await _service.GetProfileByIdAsync(Guid.Parse(profileId)) });

        // POST: api/v1/Profiles/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PutShipperUpdateAsync(Shipper shipper)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.UpdateProfileAsync(id, shipper) });
        }
    }
}