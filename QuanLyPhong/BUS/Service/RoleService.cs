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
    public class RoleService : IRoleService
    {
        private IRolesRepository _RolesRepository;
        public RoleService()
        {
            _RolesRepository = new RoleRepository();
        }
        public List<Role> GetAllRoleFromDb()
        {
            return _RolesRepository.GetAllRole();
        }
        public string AddRole(Role role)
        {
            if (_RolesRepository.CreateRole(role))
            {
                return "add success";
            }
            else
            {
                return "add fail";
            }
        }
        public string UpdateRole(Role Customer)
        {
            if (_RolesRepository.UpdadateRole(Customer))
            {
                return "Update success";
            }
            else
            {
                return "Update fail";
            }
        }
        public string RemoveRole(Guid Id)
        {
            if (_RolesRepository.DeleteRole(Id))
            {
                return "Delete success";
            }
            else
            {
                return "Delete fail";
            }
        }
    }
}
