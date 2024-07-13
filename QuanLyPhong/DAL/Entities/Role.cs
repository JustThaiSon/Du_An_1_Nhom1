using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
