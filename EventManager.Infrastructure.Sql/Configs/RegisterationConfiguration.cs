using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Domain.Entities.Event;

namespace EventManager.Infrastructure.Sql.Configs
{
    public class RegisterationConfiguration : IEntityTypeConfiguration<Registeration>
    {
        public void Configure(EntityTypeBuilder<Registeration> builder)
        {
            builder.OwnsOne(e => e.Email, n =>
                n.Property(p => p.Value).HasColumnName("Email"));
            builder.OwnsOne(e => e.PhoneNumber, n =>
                n.Property(p => p.Value).HasColumnName("PhoneNumber"));
        }
    }
}
