using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Helpers.JwToken;
using Lamar.IoC.Instances;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


namespace Domain.Concrete
{
    public class UserDomain : DomainBase, IUserDomain
    {
        private readonly Jwt _jwt;
        private readonly ClaimsPrincipal _user;
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ClaimsPrincipal user)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _jwt = new Jwt(configuration);
            _user = user;
        }

        private IUserRepository userRepository => _unitOfWork.GetRepository<IUserRepository>();
        private IRoleRepository roleRepository => _unitOfWork.GetRepository<IRoleRepository>();

        public IList<UserDTO> GetAllUsers()
        {
            IEnumerable<User> user = userRepository.GetAll();
            var test = _mapper.Map<IList<UserDTO>>(user);
            return test;
        }

        public UserDTO GetUserById(Guid id)
        {
            User user = userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetUserByEmail(string email)
        {
            User user = userRepository.GetByEmail(email);
            return _mapper.Map<UserDTO>(user);
        }

        private User getUser(string userId)
        {
            var user = userRepository.GetById(Guid.Parse(userId));
            if (user == null) throw new UnauthorizedAccessException("User not found.");
            return user;
        }

        public async Task UpdateUser(ClaimsPrincipal userClaims, UserDTO userDTO)
        {
            var userId = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) throw new UnauthorizedAccessException("User not found.");

            var user = getUser(userId);

            _mapper.Map(userDTO, user);

            if(!string.IsNullOrEmpty(userDTO.NewPassword)&& !string.IsNullOrEmpty(userDTO.ConfirmPassword))
            {
                if (!VerifyPassword(userDTO.CurrentPassword, user.Password))
                {
                    throw new UnauthorizedAccessException("Current password is incorrect.");
                }

                if (userDTO.NewPassword != userDTO.ConfirmPassword)
                {
                    throw new ArgumentException("New password and confirm password do not match.");
                }
                string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.NewPassword);
                user.Password = newPasswordHash;
            }

            foreach (var roleEnum in userDTO.Roles)
            {
                  AddRoleToUser(user, roleEnum);
            };

            userRepository.Update(user);
            _unitOfWork.Save();
        }

        private async Task AddRoleToUser(User user, RoleName roleEnum)
        {
            var roleName = Enum.GetName(typeof(RoleName), roleEnum);
            user.UserRoles.Add(new UserRole { UserId = user.UserId, RoleId = (int)roleEnum });
            _unitOfWork.Save();
        }

        public bool VerifyPassword(string providedPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, storedHash);
        }
        public async Task<List<User>> GetUsersByRole(int roleId)
        {
            return await userRepository.GetUsersByRoleAsync(roleId);

        }

        public async Task RemoveRoleFromUser(ClaimsPrincipal userClaims, int roleId)
        {
            var userId = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) throw new UnauthorizedAccessException("User not found.");

            var user =  userRepository.GetById(Guid.Parse(userId));
            if (user == null) throw new Exception("User not found.");

            var role =  roleRepository.GetById(roleId);
            if (role == null) throw new Exception("Role not found.");

            user.UserRoles.Remove(role);

            userRepository.Update(user);
            _unitOfWork.Save();
        }

        public async Task DeleteUserAccount(ClaimsPrincipal userClaims)
        {
            var userId = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) throw new UnauthorizedAccessException("User not found.");

            var user =  userRepository.GetById(Guid.Parse(userId));
            if (user == null) throw new Exception("User not found.");

            userRepository.Remove(user);
            _unitOfWork.Save();
        }
    }
}