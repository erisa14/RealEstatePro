using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserDomain
    {
        //Task Register(RegisterDto registerDto);
        //Task<string> Login(LoginDto loginDto);
        IList<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);
        UserDTO GetUserByEmail(string email);
        Task UpdateUser(ClaimsPrincipal userClaims, UserDTO userDTO);

        Task RemoveRoleFromUser(ClaimsPrincipal userClaims, int roleId);


        Task DeleteUserAccount(ClaimsPrincipal userClaims);
        Task<List<User>> GetUsersByRole(int roleId);
    }
}
