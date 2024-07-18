using BUS.IService;
using BUS.ViewModels;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Service
{
    public class RoomService : IRoomService
    {
        private IRoomRepository _roomRepository;
        private IFloorRepository _floorRepository;
        private IKindOfRoomRepository _kindOfRoomRepository;
        public RoomService()
        {
            _roomRepository = new RoomRepository();
            _floorRepository = new FloorRepository();
            _kindOfRoomRepository = new KindOfRoomRepository();
        }
        public string AddRoom(RoomViewModels room)
        {
            var RoomAdd = new Room()
            {
                Id = room.Id,
                Status = room.Status,
                RoomName = room.RoomName,
                KindOfRoomId = room.KindOfRoomId,
                FloorId = room.FloorId,
                Price = room.Price,
            };
            if (_roomRepository.CreateRoom(RoomAdd))
            {
                return "Add success";
            }
            return "Add failure";
        }

        public List<RoomViewModels> GetAllRooms()
        {
            var RoomView = from r in _roomRepository.GetAllRoom()
                           join f in _floorRepository.GetAllFloor()
                           on r.FloorId equals f.Id
                           join k in _kindOfRoomRepository.GetAllKindOfRoom()
                           on r.KindOfRoomId equals k.Id
                           select new RoomViewModels
                           {
                               Id = r.Id,
                               RoomName = r.RoomName,
                               FloorId = f.Id,
                               FloorName = f.FloorName,
                               KindOfRoomId = k.Id,
                               KindOfRoomName = k.KindOfRoomName,
                               Price = r.Price,
                               Status = r.Status
                           };
            return RoomView.ToList();
        }

        public List<Room> GetAllRoomsFromDb()
        {
            return _roomRepository.GetAllRoom();
        }

        public string RemoveRoom(Guid Id)
        {
            if (_roomRepository.DeleteRoom(Id))
            {
                return "Delete Success";
            }
            return "Delete failure";
        }
        public string UpdateRoom(RoomViewModels room)
        {
            var updateRoom = _roomRepository.GetById(room.Id);
            updateRoom.RoomName = room.RoomName;
            updateRoom.Status = room.Status;
            updateRoom.Price = room.Price;
            updateRoom.FloorId = room.FloorId;
            updateRoom.KindOfRoomId = room.KindOfRoomId;
            if (_roomRepository.UpdadateRoom(updateRoom))
            {
                return "Update success";
            }
            return "Update failcure";
        }
    }
}