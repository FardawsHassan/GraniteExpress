using Alumni.Services.Services;
using Blazored.LocalStorage;
using GraniteExpress.Data;
using GraniteExpress.Models;
using GraniteExpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System;
using TeamWorkServer.Services;

namespace GraniteExpress.Infrastructure
{
    public static class ServiceRegistry
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddRazorComponents()
                    .AddInteractiveServerComponents();
            services.AddTransient<IDatabaseResolver, DatabaseResolver>();
            services.AddScoped<CurrentUserState>();
            services.AddBlazoredLocalStorage();
;


            services.ApplyDatabaseMigrations();


            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
               .AddEntityFrameworkStores<ApplicationDbContext>();




            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>()
                    .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
                    .AddScoped<IJournalService, JournalService>()
                    .AddScoped<IAccountService, AccountService>()
                    .AddScoped<ICurrencyService, CurrencyService>()
                    .AddScoped<IClientService, ClientService>()
                    .AddScoped<IDocumentService, DocumentService>()
                    .AddScoped<ITemplateService, TemplateService>()
                    .AddScoped<ICashFlowService, CashFlowService>()
                    .AddScoped<IEmailService, EmailService>()
                    .AddScoped<IDataProtectionService, DataProtectionService>()
                    .AddScoped<IAuthenticationService, AuthenticationService>()
                    .AddScoped<IUserService, UserService>()
                    .AddMudServices();

            return services;
        }

        public static void ApplyDatabaseMigrations(this IServiceCollection services)
        {
            //using var scope = services.BuildServiceProvider().CreateScope();
            //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(m => m.UseSqlServer(e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            List<string> databases = new() {"GraniteExpress5", "GraniteExpresstemp_test" };

            foreach( var database in databases )
            {
                using var _scope = services.BuildServiceProvider().CreateScope();
                var _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                _dbContext.Database.SetConnectionString($"Server=(localdb)\\Shuvro;Database={database};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
                if (_dbContext.Database.GetMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }


                if (!_dbContext.Users.Any())
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
                    _dbContext.Users.Add(adminUser);

                    _dbContext.Roles.AddRange(
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

                    _dbContext.UserRoles.Add(
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
                    _dbContext.RoleClaims.AddRange(adminClaims);


                    _dbContext.SaveChanges();
                }
            }

        }
    }
}
