using GraniteExpress.DtoModels;
using GraniteExpress.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace GraniteExpress.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUserById(string id);
        Task<UserDto> AddUser(AddUserRequest registerRequest);
        Task<UserDto> UpdateUser(UserDto _user);
        Task<bool> DeleteUser(UserDto _user);
        Task<List<IdentityRole>> GetRoles();
        Task<bool> AddRole(string role);
        Task<bool> EditRole(IdentityRole _role, string roleName);
        Task<bool> DeleteRole(IdentityRole _role);
        Task<bool> AddClaims(string roleName, List<string> claims);
        Task<List<string>> GetClaims(IdentityRole role);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDto> AddUser(AddUserRequest registerRequest)
        {
            try
            {
                var hasUser = await _userManager.FindByEmailAsync(registerRequest.Email);

                if (hasUser is not null)
                {
                    return null;
                }

                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = registerRequest.Email,
                    UserName = registerRequest.Email.Split('@')[0],
                    NormalizedEmail = registerRequest.Email,
                    NormalizedUserName = registerRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = false,
                };


                var newUser = await _userManager.CreateAsync(user, string.IsNullOrEmpty(registerRequest.Password) ? "user123@" : registerRequest.Password);

                if (!newUser.Succeeded)
                {
                    return null;
                }

                var role = string.IsNullOrEmpty(registerRequest.Role) ? "User" : registerRequest.Role;

                var hasRole = await _roleManager.RoleExistsAsync(role);


                if (hasRole)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }

                return new()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    UserRole = role,
                    Id= user.Id
                };

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<UserDto> UpdateUser(UserDto _user)
        {
            var user = await _userManager.FindByIdAsync(_user.Id);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Any())
            {
                await _userManager.RemoveFromRoleAsync(user, role.FirstOrDefault());
            }
            
            var result = await _userManager.AddToRoleAsync(user, _user.UserRole);
            if (result.Succeeded)
            {
                return _user;
            }

            return null;
        }
        
        public async Task<bool> DeleteUser(UserDto _user)
        {
            var user = await _userManager.FindByIdAsync(_user.Id);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
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
                Dictionary<string, string> claims = new Dictionary<string, string>();
                foreach (var role in hasRoles)
                {
                    var identityRole = await _roleManager.FindByNameAsync(role);
                    var listOfClaims = await _roleManager.GetClaimsAsync(identityRole);
                    foreach(var item in listOfClaims)
                    {
                        if (!claims.Keys.Contains(item.Type))
                        {
                            claims.Add(item.Type, item.Value);
                        }
                    }
                }

                return new()
                {
                    Id = hasUser.Id,
                    UserName = hasUser.UserName ?? string.Empty,
                    Email = hasUser.Email,
                    UserRole = hasRoles.Any() ? hasRoles.First() : string.Empty,
                    Claims = claims
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UserDto>> GetUsers()
        {
            try
            {
                var users = new List<UserDto>();

                foreach (var user in _userManager.Users)
                {
                    var role = await GetRole(user);
                    users.Add(new UserDto()
                    {
                        Id = user.Id,
                        UserName = user.UserName ?? string.Empty,
                        Email = user.Email,
                        UserRole = role
                    });
                }

                return users;
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public async Task<string> GetRole(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                return roles.First();
            }

            return string.Empty;
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            if (roles.Any())
            {
                return roles;
            }

            return new();
        }
        
        public async Task<List<string>> GetClaims(IdentityRole role)
        {
            try
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                return claims.Select(x => x.Value).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddRole(string role)
        {
            var identityRole = new IdentityRole
            {
                Name = role,
                NormalizedName = role.ToUpper()
            };

            var result = await _roleManager.CreateAsync(identityRole);
            return result.Succeeded ? true : false;
        }
        
        public async Task<bool> EditRole(IdentityRole _role, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(_role.Id);
            if (role is null) {
                return false;
            }

            role.Name = roleName;
            role.NormalizedName = roleName.ToUpper();
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> DeleteRole(IdentityRole _role)
        {
            var role = await _roleManager.FindByIdAsync(_role.Id);
            if (role is null)
            {
                return false;
            }

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded ? true : false;
        }

        public async Task<bool> AddClaims(string roleName, List<string> claims)
        {
            var role = _roleManager.Roles.Where(x => x.NormalizedName == roleName.ToUpper()).FirstOrDefault();

            try
            {
                var roleExistingClaims = await _roleManager.GetClaimsAsync(role); 

                foreach(var item in roleExistingClaims)
                {
                    if (!claims.Contains(item.Value))
                    {
                        await _roleManager.RemoveClaimAsync(role, item);
                    }
                    else
                    {
                        claims.Remove(item.Value);
                    }
                }

                foreach(var item in claims)
                {
                    var newClaim = new Claim(item+"Permission", item);
                    var response = await _roleManager.AddClaimAsync(role, newClaim);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
