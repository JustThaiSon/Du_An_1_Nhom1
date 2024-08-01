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
    public class HistoryPointsService : IHistorypointService
    {
        private IHistoryPointsRepository _historyPointsRepository;
        public HistoryPointsService()
        {
            _historyPointsRepository = new HistoryPointsRepository();
        }
        public string AddhtrPoint(HistoryPoints HistoryPoints)
        {
            if (_historyPointsRepository.CreateHistoryPoints(HistoryPoints))
            {
                return "Add point success";
            }
            return "Add point failrue";
        }
        public List<HistoryPoints> GetAllhtrPointFromDb()
        {
            return _historyPointsRepository.GetAllHistoryPoints();
        }

        public string RemovehtrPoint(Guid Id)
        {
            if (_historyPointsRepository.DeleteHistoryPoints(Id))
            {
                return "Delete point success";
            }
            return "Delete point failrue";
        }

        public string UpdatehtrPoint(HistoryPoints HistoryPoints)
        {
            if (_historyPointsRepository.UpdateHistoryPoints(HistoryPoints))
            {
                return "update point success";
            }
            return "update point failrue";
        }
    }
}
