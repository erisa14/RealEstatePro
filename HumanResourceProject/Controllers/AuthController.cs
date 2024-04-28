using Domain.Concrete;
using Domain.Contracts;
using DTO.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateProProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthDomain _authDomain;

        public AuthController(IAuthDomain authDomain)
        {
            _authDomain = authDomain;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            try
            {
                await _authDomain.Register(request);
                return Ok(new { message = "User registered!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authDomain.Login(loginDto);


            if (response != null)
            {
                // return Ok(response);
                return Ok(new { response = response });

            }

            return BadRequest(new { message = "User login unsuccessful" });
        }
    }
}
