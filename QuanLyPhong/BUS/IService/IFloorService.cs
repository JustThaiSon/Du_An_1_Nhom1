using BUS.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IFloorService
    {
        string AddFloor(Floor floor);
        string RemoveFloor(Guid Id);
        string UpdateFloor(Floor floor);
        List<Floor> GetAllFloorFromDb();
    }
}
