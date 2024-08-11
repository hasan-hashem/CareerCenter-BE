using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Persistence.Identity;
using Persistence.Models;

namespace Web.Controllers
{
    public class UserController : ApiControllerBase
    {

        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            return await _authService.GetAllUsers();
          
        }
        
        [HttpPost("add")]
        public async Task<ActionResult<AuthModel>> AddUser(RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.IisAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var result = await _authService.DeleteUserAsync(userId);
            if (!result)
            {
                return NotFound("User not found");
            }
            return Ok("User deleted successfully");
        }

        [HttpPost("add-role")]
        public async Task<ActionResult<string>> AddRole(RoleModel model)
        {
            var result = await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok("Role added successfully");
        }


        [HttpGet("get-role/{userId}")]
        public async Task<ActionResult<string>> GetRole(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID is required.");

            var result = await _authService.GetRoleAsync(userId);
            if (result == "Invalid userId")
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("remove-role")]
        public async Task<ActionResult<string>> RemoveRole(RoleModel model)
        {
            var result = await _authService.RemoveRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok("Role removed successfully");
        }
    }
}
