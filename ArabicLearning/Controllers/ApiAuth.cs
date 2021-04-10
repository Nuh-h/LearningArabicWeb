using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArabicLearning.Repositories.Models;
using ArabicLearning.Repositories.Interfaces;

namespace ArabicLearning.Controllers
{
    [Authorize]
    [Route("api/[Action]")]
    [ApiController]
    public class ApiAuth : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public ApiAuth(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var user = new User();
            user.firstName = "Just test boy";
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            var user = new User();
            user.firstName = "Just test boy";
            return Ok(user);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var token = jwtAuthenticationManager.Register(user);
            if(token==null) return NotFound();
            
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpGet("{Email}/{Hashed_Password}")]
        public IActionResult Authenticate(string Email, string Hashed_Password)
        {
            var token = jwtAuthenticationManager.Authenticate(Email, Hashed_Password);
            if(token==null) return Unauthorized();
            
            return Ok(token);
        }
        
    }
}
