using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Name { get; set; }
        public Guid? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Status { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public byte[]? Img   { get; set; } 
        public DateTime? DateOfBirth { get; set; }
        public string? CCCD { get; set; }
        public MenuGender Gender { get; set; }
        public List<Orders> Orders { get; set; }
        public Role Role { get; set; }
    }
}
