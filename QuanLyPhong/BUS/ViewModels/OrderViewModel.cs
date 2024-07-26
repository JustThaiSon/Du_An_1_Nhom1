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
		public DateTime DateCreated { get; set; }
		public DateTime DatePayment { get; set; }
		public decimal ToTalPrice { get; set; }
		public string? Note { get; set; }
		public Guid EmployeeId { get; set; }
		public Guid CustomerId { get; set; }
		public decimal TotalDiscount { get; set; }
		public decimal ToTal { get; set; }
		public OrderType OrderType { get; set; }
		public Guid VoucherId { get; set; }
		public Guid HistoryPointId { get; set; }
		public Guid RoomId { get; set; }

	}
}
