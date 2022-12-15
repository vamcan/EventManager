using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Sql.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(e => e.Email, n =>
                n.Property(p => p.Value).HasColumnName("Email"));

        }
    }
}