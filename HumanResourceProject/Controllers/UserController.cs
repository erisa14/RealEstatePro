using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceProject.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDomain _userDomain;

        public UserController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }


        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var users = _userDomain.GetAllUsers();

                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById([FromRoute] Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var user = _userDomain.GetUserById(userId);

                if (user != null)
                    return Ok(user);

                return NotFound();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto register, UserRole role)
        {
            var result=await _userDomain.RegisterUserAsync(register, role);
            if (result.Succeeded)
            {
                return Ok("User registered successfully!");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userDomain.LoginUserAsync(loginDto);
            if (token != null)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid");
        }
    }
}

