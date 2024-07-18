using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{

    public interface IRolesRepository
        {
            Role GetById(Guid Id);

            List<Role> GetAllRole();
            bool CreateRole(Role role);
            bool UpdadateRole(Role role);
            bool DeleteRole(Guid Id);
            string GenerateRolesCode();

    }
}

