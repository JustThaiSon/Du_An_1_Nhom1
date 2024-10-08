﻿using BUS.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IOrderService
    {
        bool AddOrders(Orders Orders);
        string RemoveOrders(Guid Id);
        string UpdateOrders(Orders Orders);
        List<Orders> GetAllOrdersFromDb();
        List<Orders> GetByRoomId(Guid Id);
        List<OrderViewModel> GetOrdersViewModels(Guid OrderId);
        List<OrderViewModel> GetAllOrdersViewModels();
        int TotalRecord();
        List<OrderViewModel> GetAllOrdersViewModelsPage(int startRecord,int pageSize);

    }
}
