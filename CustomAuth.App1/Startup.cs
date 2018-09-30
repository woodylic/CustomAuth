using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomAuth.App1.Middlewares;
using CustomAuth.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomAuth.App1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Inject configurations
            services.AddOptions();
            
            // Inject CustomAuthClient
            services.Configure<CustomAuthClientOptions>(Configuration.GetSection("CustomAuthClient"));
            services.AddHttpClient<ICustomAuthClient, CustomAuthClient>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CustomAuthDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CustomAuthDefaults.AuthenticationScheme;
            })
            .AddCustomAuth();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            // Enable authentication
            app.UseAuthentication();
            
            app.UseMvc();
        }
    }
}