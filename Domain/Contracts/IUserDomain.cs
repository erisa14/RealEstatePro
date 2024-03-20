using DTO.UserDTO;
using Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserDomain
    {
        IList<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);

        Task<IdentityResult> RegisterUserAsync(RegisterDto model, UserRole role);
        Task<string?> LoginUserAsync(LoginDto model);
    }
}
