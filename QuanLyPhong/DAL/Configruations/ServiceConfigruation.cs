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
    public class ServiceConfigruation : IEntityTypeConfiguration<Services>
    {
        public void Configure(EntityTypeBuilder<Services> builder)
        {
            builder.ToTable("Service");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Descretion)
                .HasMaxLength(500);

            builder.Property(s => s.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(s => s.CreatedDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(s => s.Type)
                .HasMaxLength(50);
        }
    }
}
