using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AuthenAndAutho
{
    
    public class BasicAuthenticationsOptions : AuthenticationSchemeOptions
    {
        
    }
    
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationsOptions>
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;
        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationsOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, 
            ISystemClock clock,
            ICustomAuthenticationManager customAuthenticationManager) : 
            base(options, logger, encoder, clock)
        {
            _customAuthenticationManager = customAuthenticationManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHandler = Request.Headers["Authorization"];
            if(string.IsNullOrEmpty(authorizationHandler))
                return AuthenticateResult.Fail("Unauthorized");

            if(!authorizationHandler.StartsWith("bearer",StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Unauthorized");

            string token = authorizationHandler.Replace("bearer ", "");
            if (string.IsNullOrEmpty(token))
                return AuthenticateResult.Fail("Unauthorized");

            try
            {
                return ValidateToken(token);
            }
            catch (Exception ex)
            {

                return AuthenticateResult.Fail("Unauthorized");
            }
        }

        private AuthenticateResult ValidateToken(string token)
        {
            var validatedtoken = _customAuthenticationManager.Tokens.FirstOrDefault(t => t.Key == token);
            if (validatedtoken.Key == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedtoken.Value.Item1),
                new Claim(ClaimTypes.Role, validatedtoken.Value.Item2)                
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principle = new GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principle, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
