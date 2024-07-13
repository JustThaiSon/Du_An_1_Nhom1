using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string? RoomName { get; set; }
        public RoomStatus Status { get; set; }
        public Guid FloorId { get; set; }
        public Guid KindOfRoomId { get; set; }
        public decimal Price { get; set; }
        public Floor? Floor { get; set; }
        public KindOfRoom? KindOfRoom { get; set; }
        public List<Orders>? Orders { get; set; }
    }
}
