using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArabicLearning.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace ArabicLearning.Controllers
{
    
    [Route("auth/[Action]")]
    public class AuthController : Controller
    {
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult SignUp()
        {
            var user = new User();
            return View("~/Views/Auth/SignUp.cshtml",user);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            var user = new User();
            return View("~/Views/Auth/SignIn.cshtml",user);
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] User user)
        {
            user.Hashed_Password = Hash.HashPassword(user.Password); 
            var objAsJson = JsonSerializer.Serialize(user);

            var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
            content.Headers.ContentType.CharSet = string.Empty;
            System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync().Result.ToString());
            string result = "";
            using var httpResponse = await client.PostAsync("https://localhost:5001/api/register", content);
            //httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            try
            {
                result = await httpResponse.Content.ReadAsStringAsync();
            }
            catch // Could be ArgumentNullException or UnsupportedMediaTypeException
            {
                result += "HTTP Response was invalid or could not be deserialised. | ";
            };
            ViewData["result"] = "REGISTERED: "+ result;
            return View("~/Views/Auth/Welcome.cshtml", user);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] User user)
        {
            //var content = new StringContent(user.ToString(), Encoding.UTF8, "application/json");
            // var result = await client.PostAsync("authenticate", content);

            //MAKE SURE TO SOMEHOW HASH THE INCOMING PASSWORD AND THEN CHECK AGAINST SAVED HASH
            user.Hashed_Password = Hash.HashPassword(user.Password); 

            string result = "";
            using var httpResponse = await client.GetAsync("https://localhost:5001/api/authenticate/"+user.Email+"/"+user.Hashed_Password);
            //httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            try
            {
                result = await httpResponse.Content.ReadAsStringAsync();
            }
            catch // Could be ArgumentNullException or UnsupportedMediaTypeException
            {
                result += "HTTP Response was invalid or could not be deserialised. | ";
            };
            ViewData["result"] = result;
            return View("~/Views/Auth/Welcome.cshtml", user);
        }
    }
}
