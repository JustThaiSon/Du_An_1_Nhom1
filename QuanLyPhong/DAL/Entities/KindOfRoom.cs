using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class KindOfRoom
    {
        public Guid Id { get; set; }
        public string? KindOfRoomName { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PriceByHour { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
