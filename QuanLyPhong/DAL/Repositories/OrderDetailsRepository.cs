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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private MyDbContext _context;
        public OrderDetailsRepository()
        {
            _context = new MyDbContext();
        }
        public bool CreateOrderDetails(OrderDetails orderDetails)
        {
            if (orderDetails == null)
            {
                orderDetails.Id = Guid.NewGuid();
                orderDetails.OrderDetailsCode = GenerateOrderDetalsCode();
                _context.OrderDetails.Add(orderDetails);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteOrderDetails(Guid Id)
        {
            var delete = _context.OrderDetails.Find(Id);
            if (delete == null)
                return false;
            _context.OrderDetails.Remove(delete);
            _context.SaveChanges();
            return true;
        }

        public string GenerateOrderDetalsCode()
        {
            var maxCode = _context.OrderDetails
              .OrderByDescending(c => c.OrderDetailsCode)
              .Select(c => c.OrderDetailsCode)
              .FirstOrDefault();

            if (string.IsNullOrEmpty(maxCode))
            {
                return "OD001";
            }

            int maxNumber = int.Parse(maxCode.Substring(2));

            return $"CM{maxNumber + 1:D3}";
        }

        public List<OrderDetails> GetAllOrderDetails()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetails GetById(Guid Id)
        {
            return _context.OrderDetails.Find(Id);
        }

        public bool UpdateOrderDetails(OrderDetails OrderDetails)
        {
            var update = _context.OrderDetails.Find(OrderDetails.Id);
            if (update == null) return false;

            update.Id = OrderDetails.Id;
            update.OrderDetailsCode = OrderDetails.OrderDetailsCode;
            update.Quantity = OrderDetails.Quantity;
            update.OrderId = OrderDetails.OrderId;
            update.Discount = OrderDetails.Discount;
            update.TotalAmount = OrderDetails.TotalAmount;
            update.Amount = OrderDetails.Amount;
            return true;
        }
    }
}
