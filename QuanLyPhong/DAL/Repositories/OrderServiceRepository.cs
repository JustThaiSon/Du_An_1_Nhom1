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
                    orderService.OrderId = Guid.NewGuid();
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

        public bool DeleteOrderSV(Guid Id)
        {

            var delete = _context.OrderServices.Find(Id);
            if (delete != null)
            {
                _context.OrderServices.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<OrderService> GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public OrderService GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool UpdadateOrderSV(OrderService orderService)
        {
            throw new NotImplementedException();
        }
    }
}
