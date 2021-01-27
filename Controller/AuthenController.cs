using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenAndAutho.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;

        public AuthenController(ICustomAuthenticationManager customAuthenticationManager)
        {
            _customAuthenticationManager = customAuthenticationManager;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = _customAuthenticationManager.Authenticate
                (userCred.UserName, userCred.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [Authorize(Policy = "EmployeeWithMoreThan20Years")]
        [HttpPost]
        public void post([FromBody] UserCred value)
        {
            
        }

        [Authorize(Roles  = "Administrator,User")]
        [HttpGet]
       public IEnumerable<string> Get()
        {
            return new string[] { "arezoo", "1234" };
        }

    }
}
