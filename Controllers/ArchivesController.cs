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
    public class ArchivesController : ControllerBase
    {
        private readonly ArchivesService _as;
        private readonly PostsService _ps;
        public ArchivesController(ArchivesService archivesService, PostsService postsService)
        {
            _as = archivesService;
            _ps = postsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Archive>> Get(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_as.GetById(id, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Archive>> Create([FromBody] Archive newArchive)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newArchive.CreatorId = userInfo.Id;
                Archive created = _as.Create(newArchive);
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
        public async Task<ActionResult<Archive>> Edit([FromBody] Archive updated, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                updated.Id = id;
                updated.Creator = userInfo;
                return Ok(_as.Edit(updated, userInfo.Id));
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
                return Ok(_as.Delete(id, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Many-to-many
        [HttpGet("{id}/posts")]
        public ActionResult<IEnumerable<Archive>> GetPosts(int id)
        {
            try
            {
                return Ok(_ps.GetPostsByArchiveId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}