using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ArabicLearning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace ArabicLearning.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        public IActionResult Secured()
        {
            return View();
        }

        /*[HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = string.IsNullOrEmpty(returnUrl) ? returnUrl : "/home";
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string firstName, string lastName, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (firstName == "normal" && password == "name")
            {
                // claims are the things a user is or is not, not can or can't do.
                var claims = new List<Claim>
                {
                    new Claim("username", firstName),
                    new Claim(ClaimTypes.NameIdentifier, firstName)*//*,

                    //adding admin rights
                    new Claim(ClaimTypes.Role, "Admin")*//*
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }

            TempData["Error"] = "Err: username or password invalid.";

            return View("login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //return Redirect("/");
            return Redirect(@"https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:5001/");
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
