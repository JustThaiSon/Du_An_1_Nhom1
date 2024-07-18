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
    public class KindOfRoomRepository : IKindOfRoomRepository
    {
        private MyDbContext _context;
        public KindOfRoomRepository()
        {
            _context = new MyDbContext();
        }
        public bool CreateKindOfRoom(KindOfRoom KindOfRoom)
        {
            try
            {

                if (KindOfRoom != null)
                {
                    KindOfRoom.Id = Guid.NewGuid();
                    _context.KindOfRooms.Add(KindOfRoom);
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
        public bool DeleteKindOfRoom(Guid Id)
        {
            var delete = _context.KindOfRooms.Find(Id);
            if (delete != null)
            {
                _context.KindOfRooms.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<KindOfRoom> GetAllKindOfRoom()
        {
            return _context.KindOfRooms.ToList();
        }

        public KindOfRoom GetById(Guid Id)
        {
            return _context.KindOfRooms.Find(Id);
        }

        public bool UpdateKindOfRoom(KindOfRoom KindOfRoom)
        {
            var update = _context.KindOfRooms.Find(KindOfRoom.Id);
            if (update == null) return false;
            update.Id = KindOfRoom.Id;
            update.KindOfRoomName = KindOfRoom.KindOfRoomName;
            _context.SaveChanges();
            return true;
        }
    }
}
