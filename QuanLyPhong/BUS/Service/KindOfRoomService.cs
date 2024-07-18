using BUS.IService;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Service
{
    public class KindOfRoomService : IKindOfRoomService
    {
        private IKindOfRoomRepository _kindOfRoomRepository;
        public KindOfRoomService()
        {
            _kindOfRoomRepository = new KindOfRoomRepository();
        }
        public string AddKindOfRoom(KindOfRoom kindofroom)
        {
            if (_kindOfRoomRepository.CreateKindOfRoom(kindofroom))
            {
                return "Add success";
            }
            return "Add failure";

        }

        public List<KindOfRoom> GetAllKindOfRoomFromDb()
        {
            return _kindOfRoomRepository.GetAllKindOfRoom();
        }

        public string RemoveKindOfRoom(Guid Id)
        {
            if (_kindOfRoomRepository.DeleteKindOfRoom(Id))
            {
                return "Delete success";
            }
                return "Delete failure";

        }

        public string UpdateKindOfRoom(KindOfRoom kindofroom)
        {
            if (_kindOfRoomRepository.UpdateKindOfRoom(kindofroom))
            {
                return "Update success";
            }
            return "Update failure";
        }
    }
}
