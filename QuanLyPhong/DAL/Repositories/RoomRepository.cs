using DAL.Data;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private MyDbContext _context;
        public RoomRepository()
        {
            _context = new MyDbContext();
        }
        public bool CreateRoom(Room room)
        {
            try
            {
                if (room != null)
                {
                    room.Id = Guid.NewGuid();
                    _context.Rooms.Add(room);
                    _context.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteRoom(Guid Id)
        {
            var delete = _context.Rooms.Find(Id);    
            if (delete != null)
            {
                _context.Rooms.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Room> GetAllRoom()
        {
            return _context.Rooms.ToList();
        }

        public Room GetById(Guid Id)
        {
            return _context.Rooms.FirstOrDefault(x=>x.Id == Id);
        }

        public bool UpdadateRoom(Room romm)
        {
            var update = _context.Rooms.FirstOrDefault(x => x.Id == romm.Id);   

            if(update == null)return false;

            update.Id = romm.Id;
            update.RoomName = romm.RoomName;
            update.Status = romm.Status;
            update.Price = romm.Price;
            update.FloorId = romm.FloorId;
            update.KindOfRoom = romm.KindOfRoom;
            _context.SaveChanges();
            return true;
        }
    }
}
