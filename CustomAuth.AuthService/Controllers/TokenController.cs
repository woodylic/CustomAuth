using CustomAuth.AuthService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuth.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet("{token}")]
        public ActionResult<UserProfile> Get(string token)
        {
            if (token.Length != 36)
                return NotFound();

            return new UserProfile
            {
                Identifier = "31208845",
                Name = "John Snow",
                Role = "Student"
            };
        }
    }
}