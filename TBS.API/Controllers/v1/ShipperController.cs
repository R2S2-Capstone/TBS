﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.User;
using TBS.Data.Models.Post.Response;
using TBS.Data.Models.Post.Shipper;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _service;

        public ShipperController(IShipperService service)
        {
            _service = service;
        }

        // GET: api/v1/Shipper/Posts/All
        [HttpGet("Posts/All/{currentPage}/{count}")]
        public async Task<PaginatedPosts> GetShippersPosts(int currentPage, int count)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return await _service.GetAllUsersPosts(id, new Data.Models.PaginationModel() { CurrentPage = currentPage, Count = count });
        }

        // GET: api/v1/Shipper/Posts/{PostId}
        [HttpGet("Posts/{postId}")]
        public async Task<ShipperPost> GetShipperPost(int postId) => await _service.GetPostById(postId);
    }
}
