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
    public class OrderServiceConfigruation : IEntityTypeConfiguration<OrderService>
    {
        public void Configure(EntityTypeBuilder<OrderService> builder)
        {
            builder.ToTable("OrderService");

            builder.HasKey(os => new { os.OrderId, os.ServiceId });

            builder.Property(os => os.Quantity)
                .IsRequired();

            builder.Property(os => os.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(os => os.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(os => os.Service)
                .WithMany(s => s.OrderServices)
                .HasForeignKey(os => os.ServiceId);

            builder.HasOne(os => os.Orders)
                .WithMany(o => o.OrderService)
                .HasForeignKey(os => os.OrderId);
        }
    }
}
