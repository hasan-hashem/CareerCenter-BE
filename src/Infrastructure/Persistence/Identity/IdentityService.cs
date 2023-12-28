using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Identity
{
    public class IdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            return users;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : false;
        }

        private async Task<bool> DeleteUserAsync(ApplicationUser user)
        {
            await _userManager.DeleteAsync(user);
            return true;
        }

    }
}
