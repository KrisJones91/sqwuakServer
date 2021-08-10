using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sqwuakServer.Models;
using sqwuakServer.Services;


namespace sqwuakServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profs;
        private readonly PostsService _ps;
        private readonly CommentsService _cs;
        private readonly ArchivesService _archs;
        public ProfilesController(ProfilesService profs, PostsService ps, CommentsService cs, ArchivesService archs)
        {
            _profs = profs;
            _ps = ps;
            _cs = cs;
            _archs = archs;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _profs.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/posts")]
        public ActionResult<IEnumerable<Post>> GetPostsByProfileId(string id)
        {
            try
            {
                IEnumerable<Post> posts = _ps.GetByProfileId(id);
                return Ok(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/archives")]
        public ActionResult<IEnumerable<Archive>> GetArchivesByProfileId(string id)
        {
            try
            {
                IEnumerable<Archive> archives = _archs.GetByProfileId(id);
                return Ok(archives);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}