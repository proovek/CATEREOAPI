using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotylkoweBeautyAPI.Model;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MotylkoweBeautyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private UserData GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;
                return new UserData
                {
                   
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type.Equals("UserName", StringComparison.InvariantCultureIgnoreCase))?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type.Equals("UserSurname", StringComparison.InvariantCultureIgnoreCase))?.Value

                };
            }
            return null;
        }
        // GET: api/<TestController>
       // [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUser = GetCurrentUser();
                return Ok($"Witaj {currentUser.Name} {currentUser.Surname}");
            }
            return Unauthorized("Zaloguj się żeby móc zobaczyć zawartość.");
        }


        // GET api/<TestController>/5
        [HttpGet("CurrentUserPriveleges")]
        public string Get(int id)
        {
            var Data = GetCurrentUser();
            return $"value {Data.Username}";
        }

        // POST api/<TestController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonObject payload)
        {
            return Ok(payload);
        }
    }
}
