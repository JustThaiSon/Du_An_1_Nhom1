using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{

    public interface IOrdersRepository
    {
        Orders GetById(Guid Id);

        List<Orders> GetAllOrder();
        bool CreateOrder(Orders orders);
        bool UpdadateOrder(Orders orders);
        bool DeleteOrder(Guid Id);
		string GenerateOrderCode();

    }

}
