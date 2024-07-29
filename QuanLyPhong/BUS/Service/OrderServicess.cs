using BUS.IService;
using BUS.ViewModels;
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
		private readonly IServiceRepository _serviceRepository;
		private readonly IRoomRepository _roomRepository;
		private readonly IOrderServiceRepository _orderServiceRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IVoucherRepository _voucherRepository;
		private readonly IKindOfRoomRepository _kindOfRoomRepository;
		private readonly IFloorRepository _floorRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IHistoryPointsRepository _historyPointsRepository;
		public OrderServicess()
        {
			_ordersRepository = new OrdersRepository();
			_serviceRepository = new ServiceRepository();
			_roomRepository = new RoomRepository();
			_orderServiceRepository = new OrderServiceRepository();
			_employeeRepository = new EmployeeRepository();
			_voucherRepository = new VoucherRepository();
			_kindOfRoomRepository = new KindOfRoomRepository();
			_floorRepository = new FloorRepository();
			_customerRepository = new CustomerRepository();
			_historyPointsRepository = new HistoryPointsRepository();
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

		public List<OrderViewModel> GetAllOrdersViewModels()
		{
			var getAll = from o in _ordersRepository.GetAllOrder()
						 join
						 os in _orderServiceRepository.GetAllOrder() on o.Id equals os.OrderId
						 join sv in _serviceRepository.GetAllService() on os.ServiceId equals sv.Id
						 join ctm in _customerRepository.GetAllCustomer() on o.CustomerId equals ctm.Id
						 join r in _roomRepository.GetAllRoom() on o.RoomId equals r.Id
						 join kof in _kindOfRoomRepository.GetAllKindOfRoom() on r.KindOfRoomId equals kof.Id
						 join fl in _floorRepository.GetAllFloor() on r.FloorId equals fl.Id
						 join emp in _employeeRepository.GetAllEmployee() on o.EmployeeId equals emp.Id
						 join v in _voucherRepository.GetAllVouchers() on o.VoucherId equals v.Id
						 join htp in _historyPointsRepository.GetAllHistoryPoints() on o.HistoryPointId equals htp.Id
						 select new OrderViewModel
						 {
							 Id = o.Id,
							 OrderCode = o.OrderCode,
							 PayMents = o.PayMents,
							 DateCreated = o.DateCreated,
							 DatePayment = o.DatePayment,
							 ToTalPrice = o.ToTalPrice,
							 Note = o.Note,
							 Prepay = o.Prepay,
							 Rentaltype = o.Rentaltype,
							 EmployeeId = emp.Id,
							 CustomerId = ctm.Id,
							 TotalDiscount = o.TotalDiscount,
							 TotalPricePoint = o.TotalPricePoint,
							 ToTal = o.ToTal,
							 OrderType = o.OrderType,
							 VoucherId = v.Id,
							 HistoryPointId = htp.Id,
							 RoomId = r.Id,
							 CustomerName = ctm.Name,
							 EmployeeName = emp.Name,
							 QuantityService = os.Quantity,
							 PriceService = os.Price,
							 TotalPriceService = os.TotalPrice,
							 RoomName = r.RoomName,
							 KindOfRoomName = kof.KindOfRoomName,
							 FloorName = fl.FloorName,
							 PriceByHour = kof.PriceByHour,
							 PricePerDay = kof.PricePerDay,
							 NameService = sv.Name
						 };
			return getAll.ToList();
		}

		public List<Orders> GetByRoomId(Guid Id)
		{
			return _ordersRepository.GetByRoomId(Id);
		}

		public string RemoveOrders(Guid Id)
		{
			throw new NotImplementedException();
		}

		public string UpdateOrders(Orders Orders)
		{

			if (_ordersRepository.UpdadateOrder(Orders))
			{
				return "Update success";
			}
			else
			{
				return "Update failure";
			}
		}
	}
}
