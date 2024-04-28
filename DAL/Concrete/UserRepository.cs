using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using StructureMap;


namespace DAL.Concrete
{
    internal class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {

        public UserRepository(RealEstateContext dbContext) : base(dbContext)
        {
        }

        public User GetById(Guid id)
        {
            var user = context.Where(a => a.UserId == id).FirstOrDefault();
            return user;
        }

        public User GetByEmail(string email)
        {
            var user= context.Where(a=> a.Email == email).FirstOrDefault();
            return user;
        }

        public void AddUserWithRoles(User user, IEnumerable<UserRole> roles)
        {
            context.Add(user);

            foreach (var role in roles)
            {
                user.UserRoles.Add(role);
            }
        }



        public async Task<List<User>> GetUsersByRoleAsync(int roleId)
        {
            var users = await context.Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
                             .ToListAsync();

            return users;
        }

        public void RemoveRoleFromUser(User user, int roleId)
        {
            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);           
            user.UserRoles.Remove(userRole);
            
        }
    }
}