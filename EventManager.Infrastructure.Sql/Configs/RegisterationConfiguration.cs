using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EventManager.Core.Domain.Entities.Event;

namespace EventManager.Infrastructure.Sql.Configs
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.OwnsOne(e => e.Email, n =>
                n.Property(p => p.Value).HasColumnName("Email"));
            builder.OwnsOne(e => e.PhoneNumber, n =>
                n.Property(p => p.Value).HasColumnName("PhoneNumber"));
        }
    }
}
