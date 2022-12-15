using System.Text.Json.Serialization;
using EventManager.Core.Application.ServiceConfiguration;
using EventManager.Infrastructure.Sql;
using EventManager.Infrastructure.Auth;

namespace EventManager.Web.Api.Startup
{
    public static class Services
    {
        public static void Add(IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #region Services


            services.AddIdentityInfrastructureServices(configuration);
            

            services.AddEventDbContext(configuration.GetConnectionString("Default"));
            services.AddSqlInfrastructureServices();

            services.AddApplicationServices();
            #endregion
        }
    }
}
