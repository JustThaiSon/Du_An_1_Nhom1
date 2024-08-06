using DAL.Data;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private MyDbContext _context;
        public OrdersRepository()
        {
            _context = new MyDbContext();
        }
        public bool CreateOrder(Orders orders)
        {
            try
            {
                if (orders != null)
                {
                    orders.Id = Guid.NewGuid();
                    orders.OrderCode = GenerateOrderCode();
					_context.Orders.Add(orders);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.ToString());
				return false;
            }
        }

        public bool DeleteOrder(Guid Id)
        {
            var delete = _context.Orders.Find(Id);
            if (delete != null)
            {
                _context.Orders.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string GenerateOrderCode()
        {
            var maxCode = _context.Orders
               .OrderByDescending(c => c.OrderCode)
               .Select(c => c.OrderCode)
               .FirstOrDefault();

            if (string.IsNullOrEmpty(maxCode))
            {
                return "OD000001";
            }

            int maxNumber = int.Parse(maxCode.Substring(2));

            return $"OD{maxNumber + 1:D6}";
        }

        public List<Orders> GetAllOrder()
        {
            return _context.Orders.ToList();
        }

        public Orders GetById(Guid Id)
        {
            return _context.Orders.FirstOrDefault(x =>x.Id == Id);
        }
		public string GenerateOrdersCode()
		{
			var maxCode = _context.Orders
				.OrderByDescending(c => c.OrderCode)
				.Select(c => c.OrderCode)
				.FirstOrDefault();

			if (string.IsNullOrEmpty(maxCode))
			{
				return "OD0001";
			}

			int maxNumber = int.Parse(maxCode.Substring(2));

			return $"OD{maxNumber + 1:D3}";
		}
		public bool UpdadateOrder(Orders orders)
        {
            var update = _context.Orders.FirstOrDefault(o => o.Id == orders.Id);
            if (update == null) return false;
            update.Id = orders.Id;
            update.PayMents = orders.PayMents;
            update.DateCreated = orders.DateCreated;
            update.DatePayment = orders.DatePayment;
            update.ToTalPrice = orders.ToTalPrice;
            update.Note = orders.Note;
            update.Prepay = orders.Prepay;
            update.TotalPricePoint= orders.TotalPricePoint;
            update.Rentaltype= orders.Rentaltype;
            update.EmployeeId = orders.EmployeeId;
            update.CustomerId = orders.CustomerId;
            update.TotalDiscount = orders.TotalDiscount;
            update.ToTal = orders.ToTal;
            update.VoucherId = orders.VoucherId;
            update.HistoryPointId = orders.HistoryPointId;
            update.RoomId = orders.RoomId;
            _context.SaveChanges();
            return true;
        }

		public List<Orders> GetByRoomId(Guid Id)
		{
			return _context.Orders.Where(x=>x.RoomId == Id).ToList();
		}
	}
}
