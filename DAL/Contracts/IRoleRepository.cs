using Entities.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IRoleRepository : IRepository<UserRole, Guid>
    {
        UserRole GetById(int id);

        List<UserRole> GetUserRolesById(Guid userId);
    }
}
