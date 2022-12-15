using System.Text.Json.Serialization;
using EventManager.Core.Application.ServiceConfiguration;
using EventManager.Infrastructure.Auth;
using EventManager.Infrastructure.Sql;

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


           services.AddIdentityInfrastructureServices(configuration);
            

            services.AddEventDbContext(configuration.GetConnectionString("Default"));
            services.AddSqlInfrastructureServices();

            services.AddApplicationServices();
            #endregion
        }
    }
}
