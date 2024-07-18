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
    public class OrderDetailsConfigruation : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);
            builder.Property(od => od.Quantity)
              .IsRequired();

            builder.Property(od => od.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(od => od.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(od => od.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(od => od.Orders)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

        }
    }
}
