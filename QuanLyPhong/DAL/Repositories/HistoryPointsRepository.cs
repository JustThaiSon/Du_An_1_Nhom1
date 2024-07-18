using DAL.Data;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HistoryPointsRepository : IHistoryPointsRepository
    {
        private MyDbContext _context;
        public HistoryPointsRepository()
        {
            _context = new MyDbContext();   
        }
        public bool CreateHistoryPoints(HistoryPoints History)
        {
            try
            {
                if (History == null)
                {
                    return false;
                }
                History.Id = Guid.NewGuid();
                _context.HistoryPoints.Add(History);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteHistoryPoints(Guid Id)
        {
            var delete = _context.HistoryPoints.Find(Id);
            if (delete == null) return false;
            _context.HistoryPoints.Remove(delete);
            _context.SaveChanges();
            return true;
        }

        public List<HistoryPoints> GetAllHistoryPoints()
        {
            return _context.HistoryPoints.ToList();
        }

        public HistoryPoints GetById(Guid Id)
        {
            return _context.HistoryPoints.Find(Id);
        }

        public bool UpdateHistoryPoints(HistoryPoints History)
        {
            var Update = _context.HistoryPoints.Find(History.Id);
            if (Update == null) return false;
            Update.Id = History.Id;
            Update.Point = History.Point;
            Update.CreatedDate = History.CreatedDate;
            Update.CustomerId = History.CustomerId;
            _context.SaveChanges(true);
            return true;
        }
    }
}

