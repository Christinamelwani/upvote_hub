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

        public VoteNookController(IVoteNookRepository voteNookRepository)
        {
            _voteNookRepository = voteNookRepository;
        }

        [HttpGet]
        public IActionResult GetVoteNooks()
        {
            var voteNooks = _voteNookRepository.GetVoteNooks();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(voteNooks);
        }
    }
}
