using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
        internal interface IServiceRepository
        {
            Service GetById(Guid Id);

            List<Service> GetAllService();
            bool CreateService(Service  service);
            bool UpdadateService(Service service);
            bool DeleteService(Guid Id);
        }
    }

