using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CustomAuth.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CustomAuth.App1.Middlewares
{
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        public CustomAuthHandler(
            IOptionsMonitor<CustomAuthOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers[Options.TokenHeaderKey];

            if(string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail($"The {Options.TokenHeaderKey} header was missing.");
            }

            UserProfile userProfile = null;
            try
            {
                userProfile = await Options.CustomAuthClient.GetUserProfileAsync(token);
            }
            catch (HttpRequestException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }

            var identity = new ClaimsIdentity(ClaimsIssuer);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userProfile.Identifier));
            identity.AddClaim(new Claim(ClaimTypes.Name, userProfile.Name));
            identity.AddClaim(new Claim(ClaimTypes.Role, userProfile.Role));
            
            var principal = new ClaimsPrincipal(identity);
            
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
            return AuthenticateResult.Success(ticket);
        }
    }
}