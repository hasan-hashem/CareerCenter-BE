using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
