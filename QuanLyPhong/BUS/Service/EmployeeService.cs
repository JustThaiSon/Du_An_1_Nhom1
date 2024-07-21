using BUS.IService;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Service
{
    public class EmployeeService : IEmployeeService
    {
        private  IEmployeeRepository _employeeRepo;

        public EmployeeService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public List<Employee> GetAllEmployeeFromDb()
        {
            return _employeeRepo.GetAllEmployee();
        }

        public string AddEmployee(Employee Employee)
        {
            if (_employeeRepo.CreateEmployee(Employee))
            {
                return ("add success");
            }
            else
            {
                return ("add fail");
            }
        }
        public string UpdateEmployee(Employee Employee)
        {
            if (_employeeRepo.UpdateEmployee(Employee))
            {
                return ("Update success");
            }
            else
            {
                return ("Update fail");
            }
        }
        public string RemoveEmployee(Guid Id)
        {
            if (_employeeRepo.DeleteEmployee(Id))
            {
                return ("Delete success");
            }
            else
            {
                return ("Delete fail");
            }
        }

        
    }
}
