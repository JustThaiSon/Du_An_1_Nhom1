using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configruations
{
    public class RoleConfigruation : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(r => r.RoleCode)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(r => r.RoleName)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(c => c.RoleCode).IsUnique();
        }
    }
}
