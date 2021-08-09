using System;
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


    }
}