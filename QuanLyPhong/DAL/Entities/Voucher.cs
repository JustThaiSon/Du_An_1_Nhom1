using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Voucher
    {
        public Guid Id { get; set; }
        public string? VoucherCode { get; set; }
        public string? VoucherName { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? MinPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public VoucherStatus Status { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
