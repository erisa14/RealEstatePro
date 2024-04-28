using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Helpers.JwToken;
using LamarCodeGeneration.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Helpers.TokenConverter;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Domain.Concrete
{
    internal class UserDomain : DomainBase, IUserDomain
    {
        private readonly Jwt _jwt;
        private readonly ClaimsPrincipal _user;
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _jwt = new Jwt(configuration);
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

        private User getUser()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            Guid userId;
            if (userIdClaim != null)
            {
                userId = ClaimConvert.ConvertGuid(userIdClaim);
            }
            else
            {
                throw new Exception("User doesn't exist");
            }
            var user = userRepository.GetById(userId);
            return user;
        }

        public async Task UpdateUser(UserDTO userDTO)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            Guid userId;
            if (userIdClaim != null)
            {
                userId=ClaimConvert.ConvertGuid(userIdClaim);
            }
            else
            {
                throw new Exception("User doesn't exist");
            }

            User user=userRepository.GetById(userId);
            user = _mapper.Map<UserDTO, User>(userDTO, user);

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

        public async Task RemoveRoleFromUser(int roleId)
        {
            var claimId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            Guid userId;
            if (claimId != null)
            {
                userId = ClaimConvert.ConvertGuid(claimId);
            }
            else
            {
                throw new Exception("User doesn't exist");
            }

            User user = userRepository.GetById(userId);
            var role = roleRepository.GetById(roleId);

            user.UserRoles.Remove(role);
            _unitOfWork.Save();

        }



        public async Task DeleteUserAccount()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            Guid userId;
            if (userIdClaim != null)
            {
                userId = ClaimConvert.ConvertGuid(userIdClaim);
            }
            else
            {
                throw new Exception("User doesn't exist");
            }
            var user = userRepository.GetById(userId);

            var userRoles = user.UserRoles;
            foreach (var role in userRoles)
            {
                roleRepository.Remove(role);
            }
            _unitOfWork.Save();

            userRepository.Remove(user);
            _unitOfWork.Save();
        }

    }
}