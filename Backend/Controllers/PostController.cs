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
        private readonly IVoteRepository _voteRepository;
        private readonly ICommentRepository _commentRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, IVoteNookRepository voteNookRepository, IVoteRepository voteRepository, ICommentRepository commentRepository)
        {
            _voteNookRepository = voteNookRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _voteRepository = voteRepository;
            _commentRepository = commentRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostDto newPost)
        {
            // Move this to middleware soon
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
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

        [HttpGet("{postId}")]
        public IActionResult GetPost(int postId)
        {
            var post = _postRepository.GetPost(postId);

            if (post == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(post);
        }

        [HttpGet("{postId}/votes")]
        public IActionResult GetVoteCount(int postId)
        {
            var post = _postRepository.GetPost(postId);

            if (post == null)
                return NotFound();

            int voteCount = post.Votes.Count(vote => vote.Up) - post.Votes.Count(vote => !vote.Up);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(voteCount);
        }


        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId)
        {
            var post = _postRepository.GetPost(postId);

            if (post == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var vote in post.Votes)
            {
                _voteRepository.DeleteVote(vote);
            }

            foreach (var comment in post.Comments)
            {
                _commentRepository.DeleteComment(comment);
            }

            if (!_postRepository.DeletePost(post))
                return StatusCode(500, "Failed to save");

            return Ok("Successfully deleted");
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
