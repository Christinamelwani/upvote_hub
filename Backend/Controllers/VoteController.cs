using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using System.Security.Claims;

namespace Backend.Controller
{
    [ApiController]
    [Route("[controller]")]

    public class VoteController : ControllerBase
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public VoteController(IVoteRepository voteRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _voteRepository = voteRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }


        [Authorize]
        [HttpPost]
        public IActionResult CreateVote([FromBody] VoteDto newVote)
        {
            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToDictionary(c => c.Type, c => c.Value);
            string userEmail = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

            var user = _userRepository.GetUserByEmail(userEmail);
            var post = _postRepository.GetPost(newVote.PostId);

            if (post == null)
                return NotFound("Post not found");

            if (_voteRepository.VoteExists(user.Id, post.Id))
                return StatusCode(422, "User already voted");

            Vote vote = new Vote
            {
                Up = newVote.Up,
                User = user,
                Post = post,
            };

            if (!_voteRepository.CreateVote(vote))
                return StatusCode(500, "Failed to save!");

            return StatusCode(201, vote);
        }

        [Authorize]
        [HttpDelete("{voteId}")]
        public IActionResult DeleteVote(int voteId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToDictionary(c => c.Type, c => c.Value);
            string userEmail = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

            var user = _userRepository.GetUserByEmail(userEmail);
            var vote = _voteRepository.GetVote(voteId);

            if (vote == null)
                return NotFound("Vote not found");

            if (!_voteRepository.DeleteVote(vote))
                return StatusCode(500, "Failed to save!");

            return StatusCode(200, "Successfully deleted");
        }
    }
}