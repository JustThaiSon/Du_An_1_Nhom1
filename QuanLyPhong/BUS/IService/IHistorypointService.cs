using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IHistorypointService
    {
        string AddhtrPoint(HistoryPoints HistoryPoints);
        string RemovehtrPoint(Guid Id);
        string UpdatehtrPoint(HistoryPoints HistoryPoints);
        List<HistoryPoints> GetAllhtrPointFromDb();
    }
}
