using System;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace CustomAuth.App1.Middlewares
{
    public class CustomAuthPostConfigureOptions : IPostConfigureOptions<CustomAuthOptions>
    {
        public void PostConfigure(string name, CustomAuthOptions options)
        {               
            if (options.Backchannel == null)
            {
                options.Backchannel = new HttpClient();              
                options.Backchannel.Timeout = options.BackchannelTimeout;
                options.Backchannel.MaxResponseContentBufferSize = 1024 * 1024 * 1; // 1 MB
            }
        }
    }
}