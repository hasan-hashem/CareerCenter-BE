using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Identity;
using Persistence.Models;

namespace Web.Controllers
{
    [Route("api/auth")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterAsync(model);
            if (!result.IisAuthenticated)
                return BadRequest(result.Message);

            return Ok(new
            {
                Token = result.Token,
                UserId = result.UserId,
                UserName = result.UserName,
                Roles = result.Roles
            });
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.LogInAsync(model);
            if (!result.IisAuthenticated)
                return BadRequest(new
                {
                    Message = result.Message,
                    IsAuthenticated = result.IisAuthenticated
                });

            return Ok(new
            {
                Token = result.Token,
                UserId = result.UserId,
                UserName = result.UserName,
                Roles = result.Roles
            });
        }
        
        
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole(RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }


        [HttpPost("removerole")]
        public async Task<IActionResult> RemoveRole(RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RemoveRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
            //return Ok("The user has been successfully removed from the role.");
        }
    }
}
