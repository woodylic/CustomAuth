using System;
using System.Net.Http;
using CustomAuth.Client;
using Microsoft.Extensions.Options;

namespace CustomAuth.App1.Middlewares
{
    public class CustomAuthPostConfigureOptions : IPostConfigureOptions<CustomAuthOptions>
    {
        private readonly ICustomAuthClient _customAuthClient;
        
        public CustomAuthPostConfigureOptions(ICustomAuthClient customAuthClient)
        {
            _customAuthClient = customAuthClient;
        }
        
        public void PostConfigure(string name, CustomAuthOptions options)
        {
            if (options.CustomAuthClient == null)
            {
                options.CustomAuthClient = _customAuthClient;
            }
        }
    }
}