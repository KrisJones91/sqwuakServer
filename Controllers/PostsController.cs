using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sqwuakServer.Models;
using sqwuakServer.Services;

namespace sqwuakServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _ps;
        public PostsController(PostsService ps)
        {
            _ps = ps;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            try
            {
                return Ok(_ps.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetResult(int id)
        {
            try
            {
                return Ok(_ps.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> Post([FromBody] Post newPost)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newPost.CreatorId = userInfo.Id;
                Post created = _ps.Create(newPost);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}