using Blazored.LocalStorage;
using GraniteExpress.Data;
using GraniteExpress.Models;
using GraniteExpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

namespace GraniteExpress.Infrastructure
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddRazorComponents()
                    .AddInteractiveServerComponents();

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>()
                    .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
                    .AddScoped<IJournalService, JournalService>()
                    .AddScoped<IAccountService, AccountService>()
                    .AddScoped<ICurrencyService, CurrencyService>()
                    .AddScoped<IClientService, ClientService>()
                    .AddScoped<IDocumentService, DocumentService>()
                    .AddScoped<ITemplateService, TemplateService>()
                    .AddScoped<ICashFlowService, CashFlowService>()
                    .AddScoped<IAuthenticationService, AuthenticationService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<CurrentUserState>()
                    .AddBlazoredLocalStorage()
                    .AddMudServices();

            return services;
        }

        public static void ApplyDatabaseMigrations(this IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (dbContext.Database.GetMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
            if (!dbContext.Users.Any())
            {
                var adminRoleId = Guid.NewGuid().ToString();
                var adminUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = "Admin",
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    NormalizedUserName = "admin@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                };

                var passwordHasher = new PasswordHasher<User>();
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin123@");
                dbContext.Users.Add(adminUser);

                dbContext.Roles.AddRange(
                  new List<IdentityRole> {
                            new()
                            {
                                Id = adminRoleId,
                                Name = "Admin",
                                NormalizedName = "Admin".ToUpper(),
                            },
                            new()
                            {
                                Name = "User",
                                NormalizedName = "User".ToUpper(),
                            }
                  });

                dbContext.UserRoles.Add(
                   new IdentityUserRole<string>
                   {
                       UserId = adminUser.Id,
                       RoleId = adminRoleId
                   });


                List<IdentityRoleClaim<string>> adminClaims = new();

                foreach (var component in AppSettings.Components.Keys)
                {
                    adminClaims.Add(
                        new()
                        {
                            RoleId = adminRoleId,
                            ClaimType = component + "Permission",
                            ClaimValue = component
                        }    
                    );
                }
                dbContext.RoleClaims.AddRange(adminClaims);


                dbContext.SaveChanges();
            }
        }
    }
}
