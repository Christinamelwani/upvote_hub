using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteNookController : ControllerBase
    {
        private readonly IVoteNookRepository _voteNookRepository;
        private readonly IUserRepository _userRepository;

        public VoteNookController(IVoteNookRepository voteNookRepository, IUserRepository userRepository)
        {
            _voteNookRepository = voteNookRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetVoteNooks()
        {
            var voteNooks = _voteNookRepository.GetVoteNooks();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(voteNooks);
        }

        [HttpGet("{voteNookId}/posts")]
        public IActionResult GetPostsInVoteNook(int voteNookId)
        {
            if (_voteNookRepository.GetVoteNook(voteNookId) == null)
            {
                return NotFound();
            }

            var posts = _voteNookRepository.GetPostsInVoteNook(voteNookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateVoteNook([FromBody] VoteNookDto newVoteNook)
        {
            // Move this to middleware soon
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToDictionary(c => c.Type, c => c.Value);
            string userEmail = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

            var creator = _userRepository.GetUserByEmail(userEmail);

            if (_voteNookRepository.GetVoteNookByName(newVoteNook.Name) != null)
                return StatusCode(422, "Title already taken!");

            VoteNook voteNookMap = new VoteNook
            {
                Name = newVoteNook.Name,
                About = newVoteNook.About,
                Creator = creator,
            };

            if (!_voteNookRepository.CreateVoteNook(voteNookMap))
                return StatusCode(500, "Failed to save!");

            return StatusCode(201, voteNookMap);
        }

    }
}
