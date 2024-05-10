﻿using Blazored.LocalStorage;
using GraniteExpress.Infrastructure;
using GraniteExpress.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GraniteExpress
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IUserService _userService;
        private readonly CurrentUserState currentUser;

        public AuthStateProvider(ILocalStorageService localStorageService, IUserService userService, CurrentUserState _currentUser)
        {
            _localStorageService = localStorageService;
            _userService = userService;
            currentUser = _currentUser;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userId = await _localStorageService.GetItemAsync<string>(key: "AuthKey");

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _userService.GetUserById(userId);

                    if (user is not null)
                    {
                        List<Claim> listOfClaims = new()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, user.UserRole)
                        };

                        foreach(var item in user.Claims)
                        {
                            listOfClaims.Add(new Claim(item.Key, item.Value));
                        }


                        identity = new ClaimsIdentity((listOfClaims), "Authentication");
                    }

                }

                var state = new AuthenticationState(new ClaimsPrincipal(identity));
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            catch
            {
                var state = new AuthenticationState(new ClaimsPrincipal(identity));
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
        }

        public async Task SetStateAsync(string userId)
        {
            await _localStorageService.SetItemAsync(key: "AuthKey", userId);

            var state = await GetAuthenticationStateAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        public async Task ClearStateAsync()
        {
            await _localStorageService.RemoveItemAsync("AuthKey");

            var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
    }
}