using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Helpers;
using Persistence.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager,
                           IOptions<JWT> jwt,
                           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
        }


        ////////////////////////////////////
        ///

        public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user != null && await _userManager.IsInRoleAsync(user, roleName);
        }






        public async Task<string> AddRoleAsync(RoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserID);

            if (user is null || await _roleManager.RoleExistsAsync(model.Role) == false)
                return "Invalid userId or roleId";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assined in this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "something went wrong";
        }

        public async Task<string> RemoveRoleAsync(RoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserID);

            if (user is null || await _roleManager.RoleExistsAsync(model.Role) == false)
                return "Invalid userId or roleId";

            var result = await _userManager.RemoveFromRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                return string.Empty;
            }
            else
                return "An error occurred while removing the user from the role";

        }
       
        public async Task<string> GetRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return "Invalid userId";

            var roles = await _userManager.GetRolesAsync(user);
            return roles.Count > 0 ? string.Join(", ", roles) : "User has no roles";
        }

        public async Task<AuthModel> LogInAsync(LogInModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "email or password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.UserId = user.Id;
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.IisAuthenticated = true;
            authModel.Roles = rolesList.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.UserName = user.UserName;

            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered." };
            if (await _userManager.FindByEmailAsync(model.UserName) is not null)
                return new AuthModel { Message = "User is already registered." };

            var newUser = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstNmae = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var err in result.Errors)
                {
                    errors += $"{err.Description},";
                }

                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            var jwtSecurityToken = await CreateJwtToken(newUser);

            return new AuthModel
            {
                Email = newUser.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IisAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = newUser.UserName,
                UserId = newUser.Id
            };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _jwt.Issuer,
                    audience: _jwt.Aduience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                    signingCredentials: signingCredentials
                 );

            return jwtSecurityToken;
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersWithRoles = await _userManager.Users
            .Select(user => new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,                
            })
            .ToListAsync();


            return usersWithRoles;
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
