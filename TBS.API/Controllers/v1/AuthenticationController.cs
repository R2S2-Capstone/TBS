using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBS.Data.Models.User.Authentication;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("Login")]
        [Authorize]
        public async Task<IActionResult> PostLoginAsync(UserLogin userLogin)
        {
            return Ok(userLogin.FirebaseUserId);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostRegisterAsync([FromBody] string accountType)
        {
            return Ok();
        }
    }
}