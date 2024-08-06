﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configruations
{
	public class OrderConfigration : IEntityTypeConfiguration<Orders>
	{
		public void Configure(EntityTypeBuilder<Orders> builder)
		{
			builder.ToTable("Orders");

			builder.HasKey(o => o.Id);

			builder.Property(o => o.OrderCode)
				.HasMaxLength(50).IsRequired();

			builder.Property(o => o.PayMents)
				.HasMaxLength(100);

			builder.Property(o => o.DateCreated)
				.HasColumnType("datetime");

			builder.Property(o => o.DatePayment)
				.HasColumnType("datetime");

			builder.Property(o => o.ToTalPrice)
				.HasColumnType("decimal(18,2)");

			builder.Property(o => o.Note)
				.HasMaxLength(500);

			builder.Property(o => o.TotalDiscount)
				.HasColumnType("decimal(18,2)");

			builder.Property(o => o.ToTal)
				.HasColumnType("decimal(18,2)");

			builder.HasOne(o => o.Employee)
				.WithMany(e => e.Orders)
				.HasForeignKey(o => o.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(o => o.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CustomerId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(o => o.Voucher)
				.WithMany(v => v.Orders)
				.HasForeignKey(o => o.VoucherId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(o => o.HistoryPoints)
				.WithMany(h => h.Orders)
				.HasForeignKey(o => o.HistoryPointId)
				 .OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(o => o.Room)
				.WithMany(r => r.Orders)
				.HasForeignKey(o => o.RoomId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
