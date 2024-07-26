using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
        public interface IServiceRepository
        {
            Services GetById(Guid Id);

            List<Services> GetAllService();
            bool CreateService(Services  service);
            bool UpdadateService(Services service);
            bool DeleteService(Guid Id);
            Task UpdateServiceStatusAuto();
        }
    }

