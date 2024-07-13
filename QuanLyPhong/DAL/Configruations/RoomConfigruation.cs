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
    public class RoomConfigruation : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(x => x.Id);

            builder.Property(r => r.RoomName)
                .HasMaxLength(50);

            builder.Property(r => r.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r => r.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(r => r.Floor)
                .WithMany(f => f.Rooms)
                .HasForeignKey(r => r.FloorId);

            builder.HasOne(r => r.KindOfRoom)
                .WithMany(k => k.Rooms)
                .HasForeignKey(r => r.KindOfRoomId);
        }
    }
}
