﻿using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Infrastructure.Identity.Common;
using EventManager.Infrastructure.Identity.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.Infrastructure.Identity
{
    public static class StartupSetup
    {
        public static void AddEventDbContext(this IServiceCollection services, string? connectionString) =>
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)); // will be created in web project root

        public static void AddSqlInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}