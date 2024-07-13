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
    public class VoucherConfigruation : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.VoucherCode)
                .HasMaxLength(50).IsRequired();

            builder.Property(v => v.VoucherName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(v => v.DiscountRate)
                .HasColumnType("decimal(5,2)");

            builder.Property(v => v.MinPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(v => v.StartDate)
                .HasColumnType("datetime");

            builder.Property(v => v.EndDate)
                .HasColumnType("datetime");

            builder.Property(v => v.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion<string>();
        }
    }
}
