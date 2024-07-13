using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Orders
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
        public Guid HistoryPointId {  get; set; }
        public Guid RoomId {  get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<OrderService> OrderService { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public Voucher Voucher { get; set; }
        public HistoryPoints HistoryPoints { get; set; }
        public Room Room { get; set; }
    }
}
