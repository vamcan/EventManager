using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.Entities.User;
using EventManager.Infrastructure.Sql.Configs;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Sql.Common
{
    public class EventDbContext: DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> option
        )
            : base(option)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registeration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
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
