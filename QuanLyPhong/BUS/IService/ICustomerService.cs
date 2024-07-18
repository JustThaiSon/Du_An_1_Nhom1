using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface ICustomerService
    {
        string AddCustomer(Customer Customer);
        string RemoveCustomer(Guid Id);
        string UpdateCustomer(Customer Customer);
        List<Customer> GetAllCustomerFromDb();
    }
}
