using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using CoreArk.Packages.Security.Services.SecurityTokens.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace Portal.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJwtService _jwtService;
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(IJwtService jwtService, ILocalStorageService localStorageService)
        {
            _jwtService = jwtService;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal user;
            if (await _localStorageService.ContainKeyAsync(Constants.ACCESS_TOKEN_KEY))
            {
                var token = await _localStorageService.GetItemAsync<string>(Constants.ACCESS_TOKEN_KEY);

                var securityToken = new JwtSecurityToken(token);
                if (securityToken.ValidTo < DateTime.UtcNow)
                {
                    await _localStorageService.RemoveItemAsync(Constants.ACCESS_TOKEN_KEY);
                }
                else
                {
                    user = _jwtService.GetIdentity(token);
                    return await Task.FromResult(new AuthenticationState(user));
                }
            }

            var identity = new ClaimsIdentity();

            user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var user = _jwtService.GetIdentity(token);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            if (await _localStorageService.ContainKeyAsync(Constants.ACCESS_TOKEN_KEY))
            {
                await _localStorageService.RemoveItemAsync(Constants.ACCESS_TOKEN_KEY);
            }

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}