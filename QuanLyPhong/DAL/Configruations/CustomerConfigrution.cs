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
    public class CustomerConfigrution : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.CustomerCode)
                .HasMaxLength(50);

            builder.Property(c => c.Name)
                .HasMaxLength(100);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(c => c.Email)
                .HasMaxLength(100);

            builder.Property(c => c.Address)
                .HasMaxLength(200);

            builder.Property(c => c.Gender)
                .HasMaxLength(10)
                .HasConversion<string>(); ;

            builder.Property(c => c.CCCD)
                .HasMaxLength(20);

            builder.HasIndex(c => c.CustomerCode).IsUnique();
            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
