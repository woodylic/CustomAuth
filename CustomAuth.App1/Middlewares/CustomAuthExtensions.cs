using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CustomAuth.App1.Middlewares
{
    public static class CustomAuthExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder)
            => builder.AddCustomAuth(CustomAuthDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<CustomAuthOptions> configureOptions)
            => builder.AddCustomAuth(CustomAuthDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, string authenticationScheme, Action<CustomAuthOptions> configureOptions)
            => builder.AddCustomAuth(authenticationScheme, CustomAuthDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CustomAuthOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<CustomAuthOptions>, CustomAuthPostConfigureOptions>());

            return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}