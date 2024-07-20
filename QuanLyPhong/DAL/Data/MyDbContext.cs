using DAL.Configruations;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=NAMNIEMNO\\SQLEXPRESS;Initial Catalog=DuAn1;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfigrution());
            modelBuilder.ApplyConfiguration(new EmployeeConfigruation());
            modelBuilder.ApplyConfiguration(new FloorConfigruation());
            modelBuilder.ApplyConfiguration(new HistoryPointConfigruation());
            modelBuilder.ApplyConfiguration(new KindOfRoomConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfigration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfigruation());
            modelBuilder.ApplyConfiguration(new OrderServiceConfigruation());
            modelBuilder.ApplyConfiguration(new RoleConfigruation());
            modelBuilder.ApplyConfiguration(new RoomConfigruation());
            modelBuilder.ApplyConfiguration(new ServiceConfigruation());
            modelBuilder.ApplyConfiguration(new VoucherConfigruation());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<HistoryPoints> HistoryPoints { get; set; }
        public DbSet<KindOfRoom> KindOfRooms { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
    }
}
