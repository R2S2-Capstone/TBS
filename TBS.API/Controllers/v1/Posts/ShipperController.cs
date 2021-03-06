﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Models;
using TBS.Data.Models.Posts.Shipper;
using TBS.Data.Models.Posts.Request;

namespace TBS.API.Controllers.v1.Posts
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Posts/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperPostService _service;

        public ShipperController(IShipperPostService service)
        {
            _service = service;
        }

        // GET: api/v1/Posts/Shipper/{currentPage}/{pageSize}
        [HttpGet("{userFirebaseId}/{currentPage}/{pageSize}")]
        [Authorize]
        public async Task<IActionResult> GetShippersPostsAsync(string userFirebaseId, int currentPage, int pageSize) => Ok(new { result = await _service.GetAllUsersPostsAsync(userFirebaseId, new PaginationModel() { CurrentPage = currentPage, PageSize = pageSize }) });

        // GET: api/v1/Posts/Shipper/{currentPage}/{pageSize}
        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<IActionResult> GetActiveShippersPostsAsync(int currentPage, int pageSize) => Ok(new { result = await _service.GetAllActivePostsAsync(new PaginationModel() { CurrentPage = currentPage, PageSize = pageSize }) });

        // GET: api/v1/Posts/Shipper/{PostId}
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetShipperPostByIdAsync(string postId) => Ok(new { result = await _service.GetPostByIdAsync(Guid.Parse(postId)) });

        // POST: api/v1/Posts/Shipper
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostShipperCreatePostAsync(ShipperPost post)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            return Ok(new { result = await _service.CreatePostAsync(id, post) });
        }
        // POST: api/v1/Posts/Shipper/{currentPage}/{pageSize}/Search
        [HttpPost("{currentPage}/{pageSize}/Search")]
        [Authorize]
        public async Task<IActionResult> SearchAllActiveResultsAsync([FromBody] SearchModel search, int currentPage, int pageSize) => Ok(new { result = await _service.GetSearchAllActivePostsAsync(search, new PaginationModel() { CurrentPage = currentPage, PageSize = pageSize }) });

        // POST: api/v1/Posts/Shipper/{PostId}
        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> PutShipperUpdateAsync(string postId, ShipperPost post) => Ok(new { result = await _service.UpdatePostAsync(Guid.Parse(postId), post) });

        // DELETE: api/v1/Posts/Shipper/{PostId}
        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteShipperPostAsync(string postId) => Ok(new { result = await _service.DeletePostAsync(Guid.Parse(postId)) });
    }
}
