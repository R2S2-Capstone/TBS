using Microsoft.AspNetCore.Mvc;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

    }
}