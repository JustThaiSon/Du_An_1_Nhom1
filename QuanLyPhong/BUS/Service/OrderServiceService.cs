using BUS.IService;
using BUS.ViewModels;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Service
{
	public class OrderServiceService : IOrderServiceService
	{
		private IOrderServiceRepository _orderServiceRepository;
		private IServiceRepository _serviceRepository;
        public OrderServiceService()
        {
            _orderServiceRepository = new OrderServiceRepository();
			_serviceRepository = new ServiceRepository();

		}
 
		public string AddOrderService(OrderService orderService)
		{
			if (_orderServiceRepository.CreateOrderSV(orderService))
			{
				return "Add success";
			}
			return "Add failure";
		}

		public List<OrderServiceViewModel> GetAllOrderService()
		{
			var getAllOrderService = from sv in _serviceRepository.GetAllService()
									 join osv in _orderServiceRepository.GetAllOrder() on sv.Id equals osv.ServiceId
									 select new OrderServiceViewModel
									 {
										 Id = sv.Id,
										 Name = sv.Name,
										 Descretion = sv.Descretion,
										 Price = sv.Price,
										 Status = sv.Status,
										 CreatedDate = sv.CreatedDate,
										 Type = sv.Type,
										 OrderId = osv.OrderId,
										 ServiceId = osv.ServiceId,
										 Quantity = sv.Quantity,
										 QuantityOrderService = osv.Quantity,
										 PriceOrderService = osv.Price,
										 TotalPrice = osv.TotalPrice,
									 };
			return getAllOrderService.ToList();
		}

		public List<OrderService> GetAllOrderServiceFromDb()
		{
			return _orderServiceRepository.GetAllOrder();
		}

		public List<OrderService> GetOrderServicesByOrderId(Guid Id)
		{
			return _orderServiceRepository.GetByOrderId(Id);
		}

		public string RemoveOrderServicee(Guid Id)
		{
			if (_orderServiceRepository.DeleteOrderSV(Id))
			{
				return "Delete success";
			}
			return "Delete failure";
		}

		public string UpdateOrderService(OrderService orderService)
		{
			var updateOrderService = _orderServiceRepository.GetById(orderService.OrderId);
			updateOrderService.Quantity = orderService.Quantity;
			updateOrderService.Price = orderService.Price;
			updateOrderService.TotalPrice = orderService.TotalPrice;
			if (_orderServiceRepository.UpdadateOrderSV(updateOrderService))
			{
				return "Update success";
			}
			return "Update failcure";
		}

		public string UpdateOrderService(IOrderService orderService)
		{
			throw new NotImplementedException();
		}
	}
}
