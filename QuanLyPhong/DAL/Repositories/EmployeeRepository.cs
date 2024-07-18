using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MyDbContext _context;
        public EmployeeRepository()
        {
            _context = new MyDbContext();
        }

        public bool ChangeStatus(List<Employee> lstEmployee)
        {
            foreach (var item in lstEmployee)
            {
                if (item.Status == true )
                {
                    item.Status = false;
                    _context.Employees.Update(item);
                    _context.SaveChanges();
                }
                else if (item.Status == false)
                {
                    item.Status = true;
                    _context.Employees.Update(item);
                    _context.SaveChanges();
                } 
            }
            return true;
        }

        public bool CreateEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    employee.Id = Guid.NewGuid();
                    employee.EmployeeCode = GenerateEmployeeCode();
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;

            }
        }

        public bool DeleteEmployee(Guid Id)
        {
            var delete = _context.Employees.Find(Id);
            if (delete == null) return false;
            _context.Employees.Remove(delete);
            _context.SaveChanges();
            return true;

        }

        public string GenerateEmployeeCode()
        {
            var maxCode = _context.Employees
             .OrderByDescending(c => c.EmployeeCode)
             .Select(c => c.EmployeeCode)
             .FirstOrDefault();

            if (string.IsNullOrEmpty(maxCode))
            {
                return "EM001";
            }

            int maxNumber = int.Parse(maxCode.Substring(2));

            return $"CM{maxNumber + 1:D3}";
        }

        public List<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(Guid Id)
        {
            return _context.Employees.Find(Id);
        }

        public bool UpdateEmployee(Employee employee)
        {
            var Update = _context.Employees.Find(employee.Id);
            if (Update == null) return false;
            Update.Id = employee.Id;
            Update.EmployeeCode = employee.EmployeeCode;
            Update.Name = employee.Name;
            Update.RoleId = employee.RoleId;
            Update.Email = employee.Email;
            Update.Address = employee.Address;
            Update.PhoneNumber = employee.PhoneNumber;
            Update.UserName = employee.UserName;
            Update.PassWord = employee.PassWord;
            Update.Img = employee.Img;
            Update.DateOfBirth = employee.DateOfBirth;
            Update.Gender = employee.Gender;
            Update.CCCD = employee.CCCD;
            _context.SaveChanges();
            return true;
        }
    }
}
