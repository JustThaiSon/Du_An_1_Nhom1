using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IFloorRepository
    {
        Floor GetById(Guid Id);
		List<Floor> GetAllFloor();
		bool CreateFloor(Floor Floor);
		bool UpdateFloor(Floor Floor);
		bool DeleteFloor(Guid Id);
    }
}
