using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IEmployeeService
    {
        string AddEmployee(Employee Employee);
        string RemoveEmployee(Guid Id);
        string UpdateEmployee(Employee Employee);
        List<Employee> GetAllEmployeeFromDb();
    }
}
