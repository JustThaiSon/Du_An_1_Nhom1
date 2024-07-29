using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.ViewModels
{
	public class OrderViewModel
	{
		public Guid Id { get; set; }
		public string? OrderCode { get; set; }
		public string? PayMents { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DatePayment { get; set; }
		public decimal ToTalPrice { get; set; }
		public string? Note { get; set; }
		public Guid EmployeeId { get; set; }
		public string? EmployeeName { get; set; }
		public RentalTypeEnum? Rentaltype { get; set; }
		public Guid CustomerId { get; set; }
		public string? CustomerName { get; set; }
		public decimal? TotalDiscount { get; set; }
		public decimal? TotalPricePoint { get; set; }
		public decimal? Prepay { get; set; }
		public string? ServiceName { get; set; }
		public decimal? ToTal { get; set; }
		public OrderType? OrderType { get; set; }
		public Guid VoucherId { get; set; }
		public string? VoucherCode { get; set; }
		public string? VoucherName { get; set; }
		public Guid HistoryPointId { get; set; }
		public int HistoryPoint { get; set; }
		public Guid RoomId { get; set; }
		public string? RoomName { get; set; }
		public Guid FloorId { get; set; }
		public string? FloorName { get; set; }
		public Guid KindOfRoomId { get; set; } 
		public string? KindOfRoomName { get; set; }
		public decimal PricePerDay { get; set; }
		public decimal PriceByHour { get; set; }
		public int QuantityService { get; set; }
		public decimal PriceService { get; set; }
		public decimal TotalPriceService { get; set; }

		public string? NameService { get; set; }
	}
}
