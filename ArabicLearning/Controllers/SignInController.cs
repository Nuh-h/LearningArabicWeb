using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ArabicLearning.Repositories.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArabicLearning.Controllers
{
    [Route("SignIn")]
    public class SignInController : Controller
    {
        public IActionResult SignIn()
        {
            User user = new User();
            return View(user);
        }

        private IConfiguration _config;
        public SignInController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromForm] User login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return View(user);//RedirectToPage("/Welcome", new { area = "" });//RedirectToAction("Welcome","Signin", new { area = "" });
        }
        //public IActionResult SignOut()
        //{
        //    return NotFound();//RedirectToAction("Index", "Courses", new { area = "" }); ;
        //}
        //public IActionResult Welcome(User user)
        //{
        //    return View(user);//RedirectToAction("Index", "Courses", new { area = "" }); ;
        //}
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private User AuthenticateUser(User login)
        {
            User user = null;

            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.Name == "Na")
            {
                user = new User { Name = "Na Travel", Email = "n.test@coldmail.com" };
            }
            return user;
        }
    }
}
