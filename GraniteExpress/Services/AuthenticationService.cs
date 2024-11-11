using Alumni.Services.Services;
using Blazored.LocalStorage;
using GraniteExpress.DtoModels;
using GraniteExpress.Models;
using GraniteExpress.Models.Enums;
using GraniteExpress.Response;
using Microsoft.AspNetCore.Identity;
using System.Data;
namespace GraniteExpress.Services
{
    public interface IAuthenticationService
    {
        Task<UnitResponse> Register(RegisterRequest registerRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<UnitResponse> ResendConfirmationEmail(ConfimationMailRequest confimationMail);
        Task<UnitResponse> ConfirmEmail(string id, string token);
        Task<UnitResponse> ForgotPassword(ForgotPasswordRequest forgotPassword);
        Task<UnitResponse> ResetPassword(ResetPasswordRequest resetPassword, string Id, string token);
        Task<UnitResponse> ChangePassword(string currentPassword, string newPassword);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IDataProtectionService _dataProtection;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(UserManager<User> userManager,ILocalStorageService localStorageService, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IDataProtectionService dataProtection, ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _dataProtection = dataProtection;
            _logger = logger;
            _localStorageService = localStorageService;
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

                var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);

                if (existingUser is not null)
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
                    EmailConfirmed = false,
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
                _logger.LogError($"Method->Register Error->{ex.Message}");
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
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Invalid email!"
                    };
                }

                var login = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);

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
                    UserId = user.Id,
                    Message = "Login successful!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->Login Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ResendConfirmationEmail(ConfimationMailRequest confimationMail)
        {
            try
            {
                if (string.Equals(confimationMail.Email, AppSettings.Settings.AdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "System error!"
                    };
                }

                var user = await _userManager.FindByEmailAsync(confimationMail.Email);

                if (user is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                var IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

                if (IsEmailConfirmed)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Email already verified!"
                    };
                }
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encryptEmailToken = await _dataProtection.Encrypt(emailToken);
                await _emailService.SendEmailConfirmationAsync(user.Id, user.Email!, encryptEmailToken, EmailSubjectEnum.EmailConfirmation);

                return new()
                {
                    IsSuccess = true,
                    Message = "An email sent to the user with a link for registration verification"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ResendConfirmationEmail Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ConfirmEmail(string id, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                if (string.IsNullOrEmpty(token))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Token is not valid!"
                    };
                }

                var decryptEmailToken = await _dataProtection.Decrypt(token);
                var response = await _userManager.ConfirmEmailAsync(user, decryptEmailToken);

                if (!response.Succeeded)
                {
                    var errorList = response.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Email verified successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ConfirmEmail Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            try
            {
                if (string.Equals(forgotPassword.Email, AppSettings.Settings.AdminEmail, StringComparison.OrdinalIgnoreCase))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "System error!"
                    };
                }

                var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
                if (user is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encryptedResetPasswordToken = await _dataProtection.Encrypt(resetPasswordToken);

                await _emailService.SendEmailConfirmationAsync(user.Id, user.Email!, encryptedResetPasswordToken, EmailSubjectEnum.ResetPasswordEmail);
                return new()
                {
                    IsSuccess = true,
                    Message = "An email sent to the user with a link for password reset"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ForgotPassword Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<UnitResponse> ResetPassword(ResetPasswordRequest resetPassword, string id, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "User doesn't exists!"
                    };
                }

                if (string.IsNullOrEmpty(token))
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Token is not valid!"
                    };
                }

                var decryptEmailToken = await _dataProtection.Decrypt(token);
                var response = await _userManager.ResetPasswordAsync(user, decryptEmailToken, resetPassword.Password);

                if (!response.Succeeded)
                {
                    var errorList = response.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                if (await _userManager.IsLockedOutAsync(user))
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Password changed successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ResetPassword Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        
        
        public async Task<UnitResponse> ChangePassword(string currentPassword, string newPassword)
        {
            try
            {
                var userID = await _localStorageService.GetItemAsync<string>(key: "AuthKey");
                var user = await _userManager.FindByIdAsync(userID);

                var response = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!response.Succeeded)
                {
                    var errorList = response.Errors.ToList();
                    return new()
                    {
                        IsSuccess = false,
                        Message = string.Join("/n", errorList.Select(e => e.Description))
                    };
                }

                if (await _userManager.IsLockedOutAsync(user))
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Password changed successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method->ResetPassword Error->{ex.Message}");
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
