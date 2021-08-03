// using Microsoft.AspNetCore.Mvc;

// namespace sqwuakServer.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProfilesController : ControllerBase
//     {
//         private readonly ProfilesService _profs;
//         private readonly PostsService _ps;
//         private readonly CommentsService _cs;
//         private readonly ArchivesService _archs;
//         public ProfilesController(ProfilesService profs, PostsService ps, CommentsService cs, ArchivesService archs)
//         {
//             _profs = profs;
//             _ps = ps;
//             _cs = cs;
//             _archs = archs;
//         }

//         [HttpGet("{id}")]
//         public ActionResult<Profile> Get(string id)
//         {
//             try
//             {

//             }
//             catch (Expception e)
//             {
//                 return BadRequest(e.Message);
//             }
//         }
//     }
// }