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

    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }


        [Authorize]
        [HttpPost]
        public IActionResult CreateComment([FromBody] CommentDto newComment)
        {
            var identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToDictionary(c => c.Type, c => c.Value);
            string userEmail = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

            var author = _userRepository.GetUserByEmail(userEmail);
            var post = _postRepository.GetPost(newComment.PostId);

            if (post == null)
                return NotFound("Post not found");

            Comment comment = new Comment
            {
                Text = newComment.Text,
                Author = author,
                Post = post,
            };

            if (!_commentRepository.CreateComment(comment))
                return StatusCode(500, "Failed to save!");

            return StatusCode(201, comment);
        }
    }
}