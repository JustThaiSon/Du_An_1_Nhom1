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
    public class IServiceService : IServiceSevice
    {
        private IServiceRepository _serviceRepository;
        public IServiceService()
        {
            _serviceRepository = new ServiceRepository();
        }
        public string AddService(Services sv)
        {
            if (_serviceRepository.CreateService(sv))
            {
                return "Add Success ";
            }
            return "Add failure ";
        }

        public List<Services> GetAllServiceFromDb()
        {
            return _serviceRepository.GetAllService();
        }

        public string RemoveService(Guid Id)
        {
            if (_serviceRepository.DeleteService(Id))
            {
                return "Delete Success";
            }
            return "Delete failure";
        }

        public string UpdateService(Services sv)
        {
            if (_serviceRepository.UpdadateService(sv))
            {
                return "Update Success";
            }
                return "Update failure";
        }
        public async Task UpdateServiceStatusAuto()
        {
            await _serviceRepository.UpdateServiceStatusAuto();
        }
    }
}