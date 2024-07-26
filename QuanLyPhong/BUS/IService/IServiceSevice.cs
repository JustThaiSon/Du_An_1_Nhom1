using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IServiceSevice
    {
        string AddService(Services sv);
        string RemoveService(Guid Id);
        string UpdateService(Services sv);
        List<Services> GetAllServiceFromDb();
        Task UpdateServiceStatusAuto();
    }
}