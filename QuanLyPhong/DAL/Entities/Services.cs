using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Services
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public string? Descretion { get; set; }
        public decimal Price { get; set; }
        public ServiceStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Type { get; set; }
        public List<OrderService> OrderServices { get; set; }
    }
}
