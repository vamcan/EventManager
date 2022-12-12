using EventManager.Core.Application.ServiceConfiguration;
using EventManager.Infrastructure.Sql;

namespace EventManager.Web.Api.Startup
{
    public static class Services
    {
        public static void Add(IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #region Services
          
            
          
          
           
            services.AddEventDbContext(configuration.GetConnectionString("Default"));
            services.AddApplicationServices();   
            services.AddSqlInfrastructureServices();
            #endregion
        }
    }
}
