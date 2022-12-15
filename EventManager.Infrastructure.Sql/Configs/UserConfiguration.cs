using EventManager.Core.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Sql.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(e => e.PasswordHash, n =>
                n.Property(p => p.Value).HasColumnName("PasswordHash"));
        }
    }
}