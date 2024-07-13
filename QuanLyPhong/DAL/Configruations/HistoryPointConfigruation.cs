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
    public class HistoryPointConfigruation : IEntityTypeConfiguration<HistoryPoints>
    {
        public void Configure(EntityTypeBuilder<HistoryPoints> builder)
        {
            builder.ToTable("HistoryPoint");
            builder.HasKey(x=>x.Id);
            builder.Property(h => h.Point)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(h => h.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasOne(h => h.Customer)
                .WithMany(c => c.HistoryPoints)
                .HasForeignKey(h => h.CustomerId);

        }
    }
}
