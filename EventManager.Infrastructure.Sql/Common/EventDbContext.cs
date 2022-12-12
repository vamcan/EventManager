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
        public DbSet<Registeration> Registerations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegisterationConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
