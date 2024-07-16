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
    public class RoleRepository : IRolesRepository
    {
        private MyDbContext _context;
        public RoleRepository()
        {
          _context = new MyDbContext();
        }
        public bool CreateRole(Role role)
        {
            try
            {
                if (role != null)
                {
                    role.Id = Guid.NewGuid();
                    _context.Roles.Add(role);
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

        public bool DeleteRole(Guid Id)
        {
            var delete = _context.Roles.Find(Id);
            if (delete != null)
            {
                _context.Roles.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Role> GetAllRole()
        {
            return _context.Roles.ToList();
        }

        public Role GetById(Guid Id)
        {
            return  _context.Roles.Find(Id);
            
        }

        public bool UpdadateRole(Role role)
        {
            var update = _context.Roles.FirstOrDefault(x => x.Id == role.Id);   
            if(update == null) return false;
            update.Id = role.Id;
            update.RoleCode = role.RoleCode;
            update.RoleName = role.RoleName;    
            return true;
        }
    }
}
