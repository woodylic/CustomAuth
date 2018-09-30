using System;
using System.Net.Http;
using CustomAuth.Client;
using Microsoft.AspNetCore.Authentication;

namespace CustomAuth.App1.Middlewares
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public CustomAuthOptions()
        {
            ClaimsIssuer = "CustomAuth.AuthService";
        }

        public string TokenHeaderKey { get; set; } = "X-CustomAuth-Token";
        
        public ICustomAuthClient CustomAuthClient { get; set; }
        
//        public string TokenEndpoint { get; set; }
//
//        public HttpClient Backchannel { get; set; }
//
//        public TimeSpan BackchannelTimeout { get; set; } = TimeSpan.FromSeconds(60);
    }
}