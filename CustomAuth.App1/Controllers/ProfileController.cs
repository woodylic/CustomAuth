using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuth.App1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        // GET api/profile
        [HttpGet]
        public ActionResult<Dictionary<string, string>> Get()
        {
            var claims = HttpContext.User.Claims;            
            return new Dictionary<string, string>()
            {
                { "identifier", claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value },
                { "name", claims.First(c => c.Type == ClaimTypes.Name).Value },
                { "role", claims.First(c => c.Type == ClaimTypes.Role).Value }
            };
        }
    }
}