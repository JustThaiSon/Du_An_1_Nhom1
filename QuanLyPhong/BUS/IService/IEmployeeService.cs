using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    internal interface IEmployeeService
    {
        string AddEmployee(Employee Employee);
        string RemoveEmployee(Guid Id);
        string UpdateEmployee(Employee Employee);
        List<Floor> GetAllEmployeeFromDb();
    }
}
