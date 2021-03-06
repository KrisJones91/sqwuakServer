using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sqwuakServer.Models;
using sqwuakServer.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sqwuakServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ProfilesService _profilesService;
        private readonly PostsService _postsService;
        private readonly ArchivesService _archivesService;

        public AccountController(AccountService accountService, ProfilesService profilesService, PostsService postsService, ArchivesService archivesService)
        {
            _accountService = accountService;
            _profilesService = profilesService;
            _postsService = postsService;
            _archivesService = archivesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("posts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByAccount()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                IEnumerable<Post> posts = _postsService.GetPostsByAccountId(userInfo.Id);
                return Ok(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("archives")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Archive>>> GetArchivesByAccountId()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                IEnumerable<Archive> archives = _archivesService.GetArchivesByAccountId(userInfo.Id);
                return Ok(archives);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}