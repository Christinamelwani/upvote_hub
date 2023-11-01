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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IVoteNookRepository _voteNookRepository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, IVoteNookRepository voteNookRepository)
        {
            _voteNookRepository = voteNookRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostDto newPost)
        {
            // Move this to middleware soon
            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToDictionary(c => c.Type, c => c.Value);
            string userEmail = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

            var author = _userRepository.GetUserByEmail(userEmail);
            var voteNook = _voteNookRepository.GetVoteNook(newPost.VoteNookId);

            if (_postRepository.GetPostByTitle(newPost.Title) != null)
                return StatusCode(422, "Title already taken!");

            Post post = new Post
            {
                Title = newPost.Title,
                Text = newPost.Text,
                ImageUrl = newPost.ImageUrl,
                Link = newPost.Link,
                Author = author,
                VoteNook = voteNook,
            };

            if (!_postRepository.CreatePost(post))
                return StatusCode(500, "Failed to save!");

            return StatusCode(201, post);
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = _postRepository.GetPosts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("search")]
        public IActionResult GetPosts([FromQuery] string query)
        {
            var posts = _postRepository.SearchForPosts(query);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }
    }
}
