using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Route("getUserById/{userId}")]
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

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            
                await _userDomain.UpdateUser(User, userDTO);
                return Ok(); 
        }


        [Authorize]
        [HttpPost("remove/role")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] int roleId)
        {
            try
            {
                await _userDomain.RemoveRoleFromUser(User, roleId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return NotFound(new { message = "User not found." });
            }
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                await _userDomain.DeleteUserAccount(User);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log the exception if necessary
                return NotFound(new { message = "User not found." });
            }
        }



        /*
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            try
            {
                await _userDomain.Register(request);
                return Ok("User registered!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _userDomain.Login(loginDto);

            
            if (response != null)
            {
                return Ok(response);

            }

            return BadRequest(new { message = "User login unsuccessful" });
        }

        */


        [HttpGet]
        [Route("getUserByRole/{roleId}")]
        public async Task<ActionResult<List<User>>> GetUsersByRoleAsync(int roleId)
        {
            var users = await _userDomain.GetUsersByRole(roleId);
            return users;
        }
    }
}

