using Microsoft.AspNetCore.Mvc;
using TBS.Data.Interfaces.User;

namespace TBS.API.Controllers.v1.Shipper
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _service;

        public ShipperController(IShipperService service)
        {
            _service = service;
        }


    }
}
