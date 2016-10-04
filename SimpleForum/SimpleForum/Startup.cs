using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using SimpleForum.Model;
using Microsoft.EntityFrameworkCore;
using SimpleForum.Services;

namespace SimpleForum
{
    public class Startup
    {
        public const string AuthenticationScheme = "AppCookie";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();

            services.AddDotVVM()
                .ConfigureTempStorages("temp");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Data Source=.\\SQLEXPRESS; Initial Catalog=SimpleForum; Integrated Security=true"));
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<AppDbContext, int>()
                .AddDefaultTokenProviders();

            services.Add(ServiceDescriptor.Singleton<AppDbInitializer, AppDbInitializer>());
            services.Add(ServiceDescriptor.Singleton<LoginService, LoginService>());
            services.Add(ServiceDescriptor.Singleton<ForumService, ForumService>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // init database
            app.ApplicationServices.GetService<AppDbInitializer>().InitializeDatabaseAsync().Wait();

            // cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = AuthenticationScheme,
                LoginPath = new PathString("/login"),
                AccessDeniedPath = new PathString("/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.WebRootPath)
            });
        }
    }
}
