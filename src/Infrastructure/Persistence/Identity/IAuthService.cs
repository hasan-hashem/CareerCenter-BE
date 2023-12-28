using Microsoft.AspNetCore.Mvc;
using Persistence.Models;

namespace Persistence.Identity
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LogInAsync(LogInModel model);
        //Task<AuthModel> LogInWithGoogle(string credential);
        Task<string> AddRoleAsync(RoleModel model);
        Task<string> RemoveRoleAsync(RoleModel model);
        Task<string> GetUserNameAsync(string userIid);
        Task<List<UserModel>> GetAllUsers();
        Task<bool> DeleteUserAsync(string userId);


    }
}
