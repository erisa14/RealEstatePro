using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Helpers.JwToken;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class AuthDomain : DomainBase, IAuthDomain
    {

        private readonly Jwt _jwt;
        public AuthDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _jwt = new Jwt(configuration);
        }

        private IUserRepository userRepository => _unitOfWork.GetRepository<IUserRepository>();
        private IRoleRepository roleRepository => _unitOfWork.GetRepository<IRoleRepository>();


        public async Task Register(RegisterDto registerDto)
        {
            var existingUser = userRepository.Find(u => u.Email == registerDto.Email).FirstOrDefault();
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var newUser = _mapper.Map<User>(registerDto);
            newUser.UserId = Guid.NewGuid();
            newUser.Password = passwordHash;


            foreach (var roleEnum in registerDto.Roles)
            {
                AddRoleToUser(newUser, roleEnum);
            };

            var result = userRepository.Add(newUser);
            _unitOfWork.Save();
        }

        private async Task AddRoleToUser(User user, RoleName roleEnum)
        {
            var roleName = Enum.GetName(typeof(RoleName), roleEnum);
            user.UserRoles.Add(new UserRole { UserId = user.UserId, RoleId = (int)roleEnum });
            _unitOfWork.Save();
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = userRepository.GetByEmail(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new Exception("Invalid  email/password");
            }

            var roles = roleRepository.GetUserRolesById(user.UserId);
            var authClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
               new(ClaimTypes.Email, user.Email)
            };
            
            foreach (var role in roles)
            {
                string selectedRole = role.RoleId.ToString();
                authClaims.Add(new Claim(ClaimTypes.Role, selectedRole));
            }
            
            var jwt = _jwt.GenerateJwt(authClaims);
            return jwt;
        }
    }
}
