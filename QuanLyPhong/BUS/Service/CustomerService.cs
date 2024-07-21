using BUS.IService;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Service
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _CustomerRepo;

        public CustomerService()
        {
            _CustomerRepo = new CustomerRepository();
        }
        public List<Customer> GetAllCustomerFromDb()
        {
            return _CustomerRepo.GetAllCustomer();
        }

        public string AddCustomer(Customer Customer)
        {
            if (_CustomerRepo.CreateCustomer(Customer)) 
            {
                return "add success";
            }
            else
            {
                return "add fail";
            }

        }

        public string UpdateCustomer(Customer Customer)
        {
            if (_CustomerRepo.UpdateCustomer(Customer))
            {
                return "Update success";
            }
            else
            {
                return "Update fail";
            }
        }
        public string RemoveCustomer(Guid Id)
        {
            if (_CustomerRepo.DeleteCustomer(Id))
            {
                return "Delete success";
            }
            else
            {
                return "Delete fail";
            }
        }

        
    }
}
