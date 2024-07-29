using DAL.Data;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderServiceRepository : IOrderServiceRepository
    {
        private MyDbContext _context;
        public OrderServiceRepository()
        {
            _context = new MyDbContext();
        }

        public bool CreateOrderSV(OrderService orderService)
        {
            try
            {
                if (orderService != null)
                {
                    _context.OrderServices.Add(orderService);
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

        public bool DeleteOrderSV(Guid orderId, Guid serviceId)
        {

			var orderService = _context.OrderServices
			 .FirstOrDefault(os => os.OrderId == orderId && os.ServiceId == serviceId);
			if (orderService != null)
            {
                _context.OrderServices.Remove(orderService);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<OrderService> GetAllOrder()
        {
            return _context.OrderServices.ToList();
        }

        public OrderService GetById(Guid Id)
        {
           return _context.OrderServices.FirstOrDefault(x=>x.OrderId == Id);
        }

		public List<OrderService> GetByOrderId(Guid Id)
		{
			return _context.OrderServices.Where(x => x.OrderId == Id).ToList();
		}


		public bool UpdadateOrderSV(OrderService orderService)
        {
           var Exits = _context.OrderServices.FirstOrDefault(x=>x.OrderId == orderService.OrderId && x.ServiceId == orderService.ServiceId);
           if (Exits == null) return false;
			Exits.OrderId = orderService.OrderId;
            Exits.ServiceId = orderService.ServiceId;
            Exits.Quantity = orderService.Quantity;
            Exits.Price = orderService.Price;
            Exits.TotalPrice = orderService.TotalPrice;
			_context.OrderServices.Update(Exits);
			_context.SaveChanges();
            return true;

		}
    }
}
