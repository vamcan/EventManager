using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.Entities.User;
using EventManager.Infrastructure.Sql.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventManager.Infrastructure.Sql.Common
{
    public class EventDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EventDbContext(DbContextOptions<EventDbContext> option, IConfiguration configuration)
            : base(option)
        {
            _configuration = configuration;
        }
        public EventDbContext(DbContextOptions<EventDbContext> option)
            : base(option)
        {
         
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }



        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((IBaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((IBaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
