using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRoomRepository
    {
        Room GetById(Guid Id);

        List<Room> GetAllRoom();
        bool CreateRoom(Room room);
        bool UpdadateRoom(Room romm);
        bool DeleteRoom(Guid Id);
    }
}
