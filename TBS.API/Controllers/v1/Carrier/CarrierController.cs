using Microsoft.AspNetCore.Mvc;
using TBS.Data.Interfaces.User;

namespace TBS.API.Controllers.v1.Carrier
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _service;

        public CarrierController(ICarrierService service)
        {
            _service = service;
        }
    }
}
