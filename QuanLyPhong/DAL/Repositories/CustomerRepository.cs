using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private MyDbContext _context;
        public CustomerRepository()
        {
            _context = new MyDbContext();
        }

        public bool CreateCustomer(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    customer.Id = Guid.NewGuid();
                    customer.CustomerCode = GenerateCustomerCode();
                    _context.Customers.Add(customer);
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
        public bool DeleteCustomer(Guid Id)
        {
            var delete = _context.Customers.Find(Id);
            if (delete != null)
            {
                _context.Customers.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string GenerateCustomerCode()
        {
            var maxCode = _context.Customers
                .OrderByDescending(c => c.CustomerCode)
                .Select(c => c.CustomerCode)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(maxCode))
            {
                return "CM001"; 
            }

            int maxNumber = int.Parse(maxCode.Substring(2));

            return $"CM{maxNumber + 1:D3}"; 
        }

        public List<Customer> GetAllCustomer()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(Guid Id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == Id);
        }

        public bool UpdateCustomer(Customer customer)
        {
            var update = _context.Customers.FirstOrDefault(y => y.Id == customer.Id);
            if (update == null) return false;
            update.Id = customer.Id;
            update.Address = customer.Address;
            update.CustomerCode = customer.CustomerCode;
            update.Name = customer.Name;
            update.PhoneNumber = customer.PhoneNumber;
            update.Email = customer.Email;
            update.Point = customer.Point;
            update.CCCD = customer.CCCD;
            update.Gender = customer.Gender;
            _context.SaveChanges();
            return true;
        }
    }
}
