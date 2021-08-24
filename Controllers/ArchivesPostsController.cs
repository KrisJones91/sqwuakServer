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

    public class ArchivesPostsController : ControllerBase
    {
        private readonly ArchivesPostsService _APservice;

        public ArchivesPostsController(ArchivesPostsService APservice)
        {
            _APservice = APservice;
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ArchivePost>> Create([FromBody] ArchivePost newPA)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newPA.CreatorId = userInfo.Id;
                return Ok(_APservice.Create(newPA));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ArchivePost> Get(int id, string userId)
        {
            try
            {
                return Ok(_APservice.GetById(id));
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
                return Ok(_APservice.Delete(id, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}