using GraniteExpress.DtoModels;
using GraniteExpress.Models;
using GraniteExpress.Response;
using Microsoft.AspNetCore.Identity;
using System.Data;
namespace GraniteExpress.Services
{
    public interface IAuthenticationService
    {
        Task<UnitResponse> Register(RegisterRequest registerRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<UnitResponse> Register(RegisterRequest registerRequest)
        {
            try
            {
                if (string.Equals(registerRequest.Email, "admin@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "System error!"
                    };
                }

                var hasUser = await _userManager.FindByEmailAsync(registerRequest.Email);

                if (hasUser is not null)
                {
                    return new() { IsSuccess = false, Message = "User already exits! Please Log in" };
                }

                var user = new User
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Email.Split('@')[0],
                    NormalizedEmail = registerRequest.Email,
                    NormalizedUserName = registerRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                };

                var newUser = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!newUser.Succeeded)
                {
                    var errorList = newUser.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                var hasRole = await _roleManager.RoleExistsAsync("User");
                if (hasRole)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Successfully registered!"
                };

            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var hasUser = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (hasUser is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Invalid email!"
                    };
                }

                var login = await _signInManager.CheckPasswordSignInAsync(hasUser, loginRequest.Password, true);

                if (!login.Succeeded)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Password is incorrect!"
                    };
                }

                return new()
                {
                    IsSuccess = true,
                    UserId = hasUser.Id,
                    Message = "Login successful!"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
