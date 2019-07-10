﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TBS.Data.Models;
using TBS.Data.Models.Post.Carrier;
using TBS.Data.Models.Post.Response;

namespace TBS.API.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ICarrierPostService _service;

        public PostsController(ICarrierPostService service)
        {
            _service = service;
        }

        // GET: api/v1/Posts/Carrier/
        [HttpGet("Carrier")]
        public async Task<PaginatedPosts> GetCarrierPosts(PaginationModel model) => await _service.GetAllActivePosts(model);

        // GET: api/v1/Posts/Shipper
        [HttpGet("Shipper")]
        public async Task<CarrierPost> GetCarrierPost(int id) => await _service.GetPostByIdAsync(id);
    }
}
