using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBS.Data.Interfaces.User.Authentication;
using TBS.Data.Models.User.Authentication;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        [Authorize]
        public async Task<IActionResult> PostLoginAsync(UserLogin userLogin)
        {
            _authenticationService.Login(userLogin.FirebaseUserId);
            return Ok(userLogin.FirebaseUserId);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostRegisterAsync(UserRegister userRegister)
        {
            _authenticationService.Register(userRegister.FirebaseUserId);
            return Ok();
        }
    }
}