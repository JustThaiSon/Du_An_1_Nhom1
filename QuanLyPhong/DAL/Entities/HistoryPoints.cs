using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HistoryPoints
    {
        public  Guid Id { get; set; }
        public  string Point { get; set; }
        public  DateTime CreatedDate { get; set; }
        public  Guid CustomerId { get; set; }

        public List<Orders> Orders { get; set; }
        public Customer Customer { get; set; }
    }
}
