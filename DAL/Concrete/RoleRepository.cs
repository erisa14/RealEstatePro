using DAL.Contracts;
using Entities.Models;
using Helpers;
using Microsoft.EntityFrameworkCore;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class RoleRepository : BaseRepository<UserRole, Guid>, IRoleRepository
    {
        public RoleRepository(RealEstateContext dbContext) : base(dbContext)
        {
        }

        public UserRole GetById(int id)
        {
            var role = context.Where(a => a.RoleId == id).FirstOrDefault();
            return role;
        }
 
         public List<UserRole> GetUserRolesById(Guid userId)
         {
            var userRoles = context.Where(a => a.UserId == userId).ToList();
            return userRoles;
         }
        
    }
}
