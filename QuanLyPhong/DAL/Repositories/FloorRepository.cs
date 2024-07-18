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
    public class FloorRepository : IFloorRepository
    {
        private MyDbContext _context;
        public FloorRepository()
        {
            _context = new MyDbContext();
        }
        public bool CreateFloor(Floor Floor)
        {
            try
            {
                if (Floor != null)
                {
                    Floor.Id = Guid.NewGuid();
                    _context.Floors.Add(Floor);
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

        public bool DeleteFloor(Guid Id)
        {
            var delete = _context.Floors.Find(Id);
            if (delete == null) return false;
            _context.Floors.Remove(delete);
            _context.SaveChanges();
            return true;
        }

        public List<Floor> GetAllFloor()
        {
            return _context.Floors.ToList();
        }

        public Floor GetById(Guid Id)
        {
            return _context.Floors.Find(Id);
        }

        public bool UpdateFloor(Floor Floor)
        {
            var update = _context.Floors.Find(Floor);
            if (update == null) return false;
            update.Id = Floor.Id;
            update.FloorName = Floor.FloorName;
            _context.SaveChanges();
            return true;
        }
    }
}
