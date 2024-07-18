using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
	public interface ICustomerRepository
	{
		Customer GetById(Guid Id);
		List<Customer> GetAllCustomer();
		bool CreateCustomer(Customer customer);
		bool UpdateCustomer(Customer customer);
		bool DeleteCustomer(Guid Id);
		string GenerateCustomerCode();

    }
}
