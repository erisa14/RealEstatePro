using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;


namespace Domain.Concrete
{
    public class UserDomain : DomainBase, IUserDomain
    {
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IUserRepository userRepository => _unitOfWork.GetRepository<IUserRepository>();
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

        public async Task<string?> LoginUserAsync(LoginDto model)
        {
            //kontrollojme nese useri ekziston
            // krijo nje method ne repository

            // JWT token
            //        var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //};

            // Shtojme rolet
            // TODO: krijo method per getroles
            //var userRoles = await _userManager.GetRolesAsync(user);
            //authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //var token = new JwtSecurityToken(
            //    issuer: _configuration["JWT:ValidIssuer"],
            //    audience: _configuration["JWT:ValidAudience"],
            //    expires: DateTime.Now.AddHours(3),
            //    claims: authClaims,
            //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);
            return null;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDto model, UserRole role)
        {
            //    //Kontrollojme nese nje user me kete email ekziston ne db
            //    var userExists = await _userManager.FindByEmailAsync(model.Email);
            //    if (userExists != null)
            //    {
            //        return IdentityResult.Failed(new IdentityError { Description = "A user with this email already exists!" });
            //    }

            //    var user = new User
            //    {
            //        Username = model.UserName,
            //        Name=model.FirstName,
            //        LastName=model.LastName,
            //        Email=model.Email
            //    };

            //   var result= await _userManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        await _userManager.AddToRoleAsync(user, role.ToString());
            //    }
            //    return result;
            //}
            return null;
        }
    }
}