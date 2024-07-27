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
	public class OrderServicess : IOrderService
	{
		private readonly IOrdersRepository _ordersRepository;
        public OrderServicess()
        {
			_ordersRepository = new OrdersRepository();
		}
        public string AddOrders(Orders Orders)
		{
            if (_ordersRepository.CreateOrder(Orders))
            {
				return "add success";
			}
			else
			{
				return "add failure";
			}
		}
		public List<Orders> GetAllOrdersFromDb()
		{
			return _ordersRepository.GetAllOrder();
		}

		public List<Orders> GetByRoomId(Guid Id)
		{
			return _ordersRepository.GetByRoomId(Id);
		}

		public string RemoveOrders(Guid Id)
		{
			throw new NotImplementedException();
		}

		public string UpdateKindOfRoom(Orders Orders)
		{
			throw new NotImplementedException();
		}
        
        }
    }

