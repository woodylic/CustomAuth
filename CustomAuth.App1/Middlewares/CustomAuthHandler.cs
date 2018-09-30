using System;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CustomAuth.App1.Models;
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

            var baseUri = new Uri(Options.TokenEndpoint);
            var authUri = new Uri(baseUri, token);
            var response = await Options.Backchannel.GetAsync(authUri);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return AuthenticateResult.Fail($"The {Options.TokenHeaderKey} token is invalid or expired.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var userProfile = JsonConvert.DeserializeObject<UserProfile>(json);

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