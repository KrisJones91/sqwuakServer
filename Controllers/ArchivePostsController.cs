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
    }
}