using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? CustomerCode { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public MenuGender Gender { get; set; }
        public string? CCCD { get; set; }
        public int Point { get; set; }
        public List<Orders> Orders { get; set; }
        public List<HistoryPoints> HistoryPoints { get; set; }

    }
}
