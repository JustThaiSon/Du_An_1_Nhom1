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
    public class KindOfRoomConfiguration : IEntityTypeConfiguration<KindOfRoom>
    {
        public void Configure(EntityTypeBuilder<KindOfRoom> builder)
        {
            builder.ToTable("KindOfRoom");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.KindOfRoomName).IsRequired().HasMaxLength(100);
        }
    }
}
