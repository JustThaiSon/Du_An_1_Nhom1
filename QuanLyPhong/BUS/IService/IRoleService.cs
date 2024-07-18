using BUS.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IRoleService
    {
        string AddRole(Role role);
        string RemoveRole(Guid Id);
        string UpdateRole(Role role);
        List<Role> GetAllRoleFromDb();
        List<Role> GetAllRole();
    }
}
