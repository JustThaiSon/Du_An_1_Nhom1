﻿using BUS.Service;
using BUS.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
	public interface IOrderServiceService
	{
		string AddOrderService(OrderService orderService); 
		string RemoveOrderServiceeAll(Guid orderId);

        string RemoveOrderServicee(Guid orderId, Guid serviceId);
        string UpdateOrderService(OrderService orderService);
		List<OrderServiceViewModel> GetAllOrderService();
		List<OrderService> GetOrderServicesByOrderId(Guid Id);
	}
}
