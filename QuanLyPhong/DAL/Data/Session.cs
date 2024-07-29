using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class Session
    {
        public static string? UserName { get; set; }
        public static Guid UserId { get; set; }
        public static string? EmployeeCode { get; set; }
        public static string? Name { get; set; }
        public static string? RoleCode { get; set; }
        public static string? PassWord { get; set; }
    }
}
