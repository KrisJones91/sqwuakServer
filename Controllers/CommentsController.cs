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
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService _cs;
        public CommentsController(CommentsService cs)
        {
            _cs = cs;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> Get()
        {
            try
            {
                return Ok(_cs.GetComments());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetResult(int id)
        {
            try
            {
                return Ok(_cs.GetCommentsById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comment>> Create([FromBody] Comment newComment)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newComment.CreatorId = userInfo.Id;
                Comment created = _cs.Create(newComment);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Comment>> Edit([FromBody] Comment updated, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                updated.Id = id;
                updated.Creator = userInfo;
                return Ok(_cs.Edit(updated, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_cs.Delete(id, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}