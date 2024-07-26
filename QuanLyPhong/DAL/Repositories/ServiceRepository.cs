using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private MyDbContext _context;
        public ServiceRepository()
        {
            _context = new MyDbContext();
        }
        public bool CreateService(Services service)
        {
            try
            {
                if(service != null)
                {
                    service.Id = Guid.NewGuid();
                    _context.Services.Add(service);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool DeleteService(Guid Id)
        {
            var delete = _context.Services.FirstOrDefault(s => s.Id == Id);
            if (delete != null)
            { 
                _context.Services.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;   
        }

        public List<Services> GetAllService()
        {
            return _context.Services.ToList();
        }

        public Services GetById(Guid Id)
        {
            return _context.Services.FirstOrDefault(x => x.Id == Id);
        }

        public bool UpdadateService(Services service)
        {
            var update = _context.Services.FirstOrDefault(x=>x.Id == service.Id);   

            if (update == null) return false;
            update.Id = service.Id;
            update.Name = service.Name;
            update.Descretion = service.Descretion;
            update.Quantity = service.Quantity;
            update.Status = service.Status;
            update.Price = service.Price;
            update.CreatedDate = service.CreatedDate;
            update.Type = service.Type; 
            _context.SaveChanges();
            return true;
        }
        public async Task UpdateServiceStatusAuto()
        {
            var services = await _context.Services.ToListAsync();
            foreach (var service in services)
            {
                if (service.Quantity == 0)
                {
                    service.Status = ServiceStatus.OutOfStock; 
                }
                else
                {
                    service.Status = ServiceStatus.Available; 
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
