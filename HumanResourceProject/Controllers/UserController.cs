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
    [Route("api/[controller]")]
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

        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            
               await _userDomain.UpdateUser(userDTO);
                
                return Ok();

            
        }


        [Authorize]
        [HttpPost("remove/role")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] int roleId)
        {
            try
            {
                await _userDomain.RemoveRoleFromUser(roleId);
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
                await _userDomain.DeleteUserAccount();
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log the exception if necessary
                return NotFound(new { message = "User not found." });
            }
        }



        [HttpGet]
        [Route("getUserByRole/{roleId}")]
        public async Task<ActionResult<List<User>>> GetUsersByRoleAsync(int roleId)
        {
            var users = await _userDomain.GetUsersByRole(roleId);
            return users;
        }
    }
}

