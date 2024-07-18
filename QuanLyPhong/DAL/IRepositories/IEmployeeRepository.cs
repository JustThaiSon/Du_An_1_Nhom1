using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(Guid Id);
        List<Employee> GetAllEmployee();
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Guid Id);
        bool ChangeStatus(List<Employee> lstEmployee);
        string GenerateEmployeeCode();

    }
}
