using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.ViewModels
{
	public class OrderServiceViewModel
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Descretion { get; set; }
		public decimal Price { get; set; }
		public ServiceStatus Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public string? Type { get; set; }
		public int? Quantity { get; set; }
		public Guid OrderId { get; set; }
		public Guid ServiceId { get; set; }
		public int QuantityOrderService { get; set; }
		public decimal PriceOrderService { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
