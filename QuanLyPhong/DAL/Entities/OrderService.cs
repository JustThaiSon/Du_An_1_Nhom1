using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OrderService
    {
        public Guid OrderId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public Services Service { get; set; }
        public Orders Orders { get; set; }

    }
}
