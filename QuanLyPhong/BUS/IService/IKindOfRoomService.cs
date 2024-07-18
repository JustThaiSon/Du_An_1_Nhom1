using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IKindOfRoomService
    {
        string AddKindOfRoom(KindOfRoom kindofrom);
        string RemoveKindOfRoom(Guid Id);
        string UpdateKindOfRoom(KindOfRoom kindofrom);
        List<KindOfRoom> GetAllKindOfRoomFromDb();
    }
}
