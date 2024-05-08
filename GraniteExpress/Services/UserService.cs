using GraniteExpress.DtoModels;
using GraniteExpress.Models;
using GraniteExpress.Response;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace GraniteExpress.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            try
            {
                var hasUser = await _userManager.FindByIdAsync(id);
                
                if (hasUser is null)
                {
                    return null;
                }

                var hasRoles = await _userManager.GetRolesAsync(hasUser);

                return new()
                {
                    Id = hasUser.Id,
                    UserName = hasUser.UserName ?? string.Empty,
                    Email = hasUser.Email,
                    UserRole = hasRoles.Any() ? hasRoles.First() : string.Empty
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
