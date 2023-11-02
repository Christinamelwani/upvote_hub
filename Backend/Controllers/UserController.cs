using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationDto newUser)
        {
            if (newUser == null)
                return BadRequest("Please enter a valid email, password, and avatarlink");

            var matchingUser = _userRepository
                .GetUsers()
                .Where(u => u.Email == newUser.Email)
                .FirstOrDefault();

            if (matchingUser != null)
            {
                return StatusCode(422, "Email already registered!");
            }

            var user = new User
            {
                Email = newUser.Email,
                Username = newUser.Username,
                AvatarUrl = newUser.AvatarUrl,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password)
            };

            if (!_userRepository.CreateUser(user))
            {
                return StatusCode(500, "Failed to save");
            }

            return StatusCode(201, user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto credentials)
        {
            if (credentials == null)
                return BadRequest("Please enter valid credentials");

            var matchingUser = _userRepository.GetUserByEmail(credentials.Email);

            if (matchingUser == null)
            {
                return StatusCode(401, "Email and password do not match");
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(
                credentials.Password,
                matchingUser.HashedPassword
            );

            if (!passwordMatch)
                return StatusCode(401, "Email and password do not match");

            var token = GenerateJwtToken(credentials.Email);

            return Ok(new { Token = token });
        }

        [HttpGet("{userId}/posts")]
        public IActionResult GetPostsByUser(int userId)
        {
            if (_userRepository.GetUser(userId) == null)
            {
                return NotFound();
            }

            var posts = _userRepository.GetPostsByUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{userId}/comments")]
        public IActionResult GetCommentsByUser(int userId)
        {
            if (_userRepository.GetUser(userId) == null)
            {
                return NotFound();
            }

            var comments = _userRepository.GetCommentsByUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }

        [Authorize]
        [HttpGet]

        public IActionResult GetUserData()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            var claims = identity.Claims.ToDictionary(c => c.Type, c => c.Value);
            string userEmail = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

            var author = _userRepository.GetUserByEmail(userEmail);

            return Ok(author);
        }

        private string GenerateJwtToken(string email)
        {
            var secretKey = "YourSecretKeyHereYourSecretKeyHereYourSecretKeyHere";
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
