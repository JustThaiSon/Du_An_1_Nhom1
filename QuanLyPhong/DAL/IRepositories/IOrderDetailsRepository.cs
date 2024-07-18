using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IOrderDetailsRepository
    {
        OrderDetails GetById(Guid Id);
        List<OrderDetails> GetAllOrderDetails();
        bool CreateOrderDetails(OrderDetails OrderDetails);
        bool UpdateOrderDetails(OrderDetails OrderDetails);
        bool DeleteOrderDetails(Guid Id);

    }
}
