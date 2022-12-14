using EventManager.Core.Domain.Contracts.Repository;
using Microsoft.Extensions.DependencyInjection;
using EventManager.Infrastructure.Common;
using EventManager.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddEventDbContext(this IServiceCollection services, string? connectionString) =>
            services.AddDbContext<EventDbContext>(options =>
                options.UseSqlServer(connectionString)); // will be created in web project root

        public static void AddSqlInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
