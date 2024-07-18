using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.ViewModels
{
    public class RoomViewModels
    {
        public Guid Id { get; set; }
        public string? RoomName { get; set; }
        public RoomStatus Status { get; set; }
        public Guid FloorId { get; set; }
        public string? FloorName { get; set; }
        public Guid KindOfRoomId { get; set; }
        public string? KindOfRoomName { get; set; }
        public decimal Price { get; set; }
    }
}
