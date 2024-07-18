using BUS.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IRoomService
    {
        string AddRoom(RoomViewModels room);
        string RemoveRoom(Guid Id);
        string UpdateRoom(RoomViewModels room);
        List<Room> GetAllRoomsFromDb();
        List<RoomViewModels> GetAllRooms();
    }
}
