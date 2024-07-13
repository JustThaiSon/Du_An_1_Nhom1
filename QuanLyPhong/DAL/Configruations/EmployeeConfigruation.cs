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
    public class EmployeeConfigruation : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.EmployeeCode)
              .HasMaxLength(50).IsRequired();
            builder.Property(e => e.Name)
                .HasMaxLength(100).IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(100).IsRequired();

            builder.Property(e => e.Address)
                .HasMaxLength(200).IsRequired();

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(15).IsRequired();

            builder.Property(e => e.UserName)
                .HasMaxLength(50).IsRequired();

            builder.Property(e => e.PassWord)
                .HasMaxLength(100).IsRequired();

            builder.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10)
                .HasConversion<string>();

            builder.Property(e => e.CCCD)
                .HasMaxLength(20).IsRequired();

            builder.Property(e => e.Img)
                .HasColumnType("varbinary(max)").IsRequired();

            builder.Property(e => e.DateOfBirth)
                .HasColumnType("datetime").IsRequired();


            builder.HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId);

            builder.HasIndex(e => e.EmployeeCode).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.UserName).IsUnique();
        }
    }
}
