using System.Text.Json.Serialization;
using EventManager.Core.Application.ServiceConfiguration;
using EventManager.Infrastructure.Sql;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EventManager.Web.App.Startup
{
    public static class Services
    {
        public static void Add(IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            #region Services



            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie("Cookies", options =>
                {
                    options.Cookie.Name = "auth_cookie";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.LoginPath = "/Home/Index";
                    options.AccessDeniedPath = "/Home/Index";
                    options.LogoutPath = "/Home/Index";
                    options.SlidingExpiration = true;
                });


            services.AddEventDbContext(configuration.GetConnectionString("Default"));
            services.AddSqlInfrastructureServices();

            services.AddApplicationServices();
            #endregion
        }
    }
}
