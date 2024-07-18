using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IKindOfRoomRepository
    {
        KindOfRoom GetById(Guid Id);
        List<KindOfRoom> GetAllKindOfRoom();
        bool CreateKindOfRoom(KindOfRoom KindOfRoom);
        bool UpdateKindOfRoom(KindOfRoom KindOfRoom);
        bool DeleteKindOfRoom(Guid Id);
    }
}
