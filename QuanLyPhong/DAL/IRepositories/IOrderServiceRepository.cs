using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{

    public interface IOrderServiceRepository
        {
            OrderService GetById(Guid Id);

            List<OrderService> GetAllOrder();
            bool CreateOrderSV(OrderService orderService );
            bool UpdadateOrderSV(OrderService orderService);
            bool DeleteOrderSV(Guid Id);

        }
    }

