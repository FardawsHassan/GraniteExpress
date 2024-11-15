﻿using Alumni.Services.Services;
using Blazored.LocalStorage;
using CarerHub.Services.ModelMapper;
using GraniteExpress.Data;
using GraniteExpress.Models;
using GraniteExpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using TeamWorkServer.Services;

namespace GraniteExpress.Infrastructure
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<CurrentUserState>();
            services.AddScoped<IDatabaseResolver, DatabaseResolver>();
            services.AddBlazoredLocalStorage();
            services.AddRazorComponents()
                    .AddInteractiveServerComponents();

            services.ApplyDatabaseMigrations();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
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
                    .AddCascadingAuthenticationState()
                    .AddAutoMapper(typeof(MapperProfile))
                    .AddMudServices();

            return services;
        }

        public static void ApplyDatabaseMigrations(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(m => m.UseSqlServer(e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            foreach (var database in AppSettings.Databases.Keys)
            {
                using var _scope = services.BuildServiceProvider().CreateScope();
                var _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var _databaseResolver = _scope.ServiceProvider.GetRequiredService<IDatabaseResolver>();
                _dbContext.Database.SetConnectionString(_databaseResolver.GetConnectionString(database));
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
