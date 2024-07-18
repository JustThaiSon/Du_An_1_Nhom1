using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IHistoryPointsRepository
    {
        HistoryPoints GetById(Guid Id);
        List<HistoryPoints> GetAllHistoryPoints();
        bool CreateHistoryPoints(HistoryPoints History);
        bool UpdateHistoryPoints(HistoryPoints History);
        bool DeleteHistoryPoints(Guid Id);
    }
}
