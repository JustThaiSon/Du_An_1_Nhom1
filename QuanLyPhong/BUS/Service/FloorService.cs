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
    public class FloorService : IFloorService
    {
        private IFloorRepository _floorRepository;
        public FloorService()
        {
            _floorRepository = new FloorRepository();
        }
        public string AddFloor(Floor floor)
        {
            if (_floorRepository.CreateFloor(floor))
            {
                return "Add success";
            }
            return "Add failure";

        }
        public List<Floor> GetAllFloorFromDb()
        {
            return _floorRepository.GetAllFloor();
        }
        public string RemoveFloor(Guid Id)
        {
            if (_floorRepository.DeleteFloor(Id))
            {
                return "Delete success";
            }
            return "Delete failure";

        }
        public string UpdateFloor(Floor floor)
        {
            if (_floorRepository.UpdateFloor(floor))
            {
                return "Update success";
            }
            return "Update failure";
        }
    }
}
