using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Infrastructure.Sql.Common;
using EventManager.Infrastructure.Sql.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.IntegrationTests.Base
{
    public class BaseRepositoryFixture : IDisposable
    {
        public EventDbContext DbContext;
        public IEventRepository EventRepository;
        public IUserRepository UserRepository;

      
        protected static DbContextOptions<EventDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<EventDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

       public void Build()
        {
            var options = CreateNewContextOptions();
            DbContext = new EventDbContext(options);
            EventRepository = new EventRepository(DbContext);
            UserRepository = new UserRepository(DbContext);
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }

}
