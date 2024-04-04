using DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IAuthDomain
    {
        Task Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
    }
}
