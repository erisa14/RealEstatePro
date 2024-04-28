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
        IList<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);
        UserDTO GetUserByEmail(string email);
        Task UpdateUser(UserDTO userDTO);

        Task RemoveRoleFromUser(int roleId);


        Task DeleteUserAccount();
        Task<List<User>> GetUsersByRole(int roleId);
    }
}
