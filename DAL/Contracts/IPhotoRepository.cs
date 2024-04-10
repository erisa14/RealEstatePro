using Entities.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        User GetById(Guid id);

        User GetByEmail(string email);

        void AddUserWithRoles(User user, IEnumerable<UserRole> roles);

        Task<List<User>> GetUsersByRoleAsync(int roleId);


    }
}
