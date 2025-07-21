using TriviaRoyaleGame.Client.Business.Extensions.LocalStorage;
using Blazored.LocalStorage;
using TriviaRoyaleGame.Client.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Hybrid.Components.Authentication
{
    public class BackOfficeAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private ClaimsPrincipal Anonymous { get; set; } = new ClaimsPrincipal(new ClaimsIdentity());
        public BackOfficeAuthenticationStateProvider(ILocalStorageService? localStorageService)
        {
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userLogged = await _localStorageService.GetItemDecryptedAsync<UserViewModel>("userLogged");
                if(userLogged == null)
                {
                    return await Task.FromResult(new AuthenticationState(Anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Email, userLogged.Email),
                    new Claim(ClaimTypes.Role, userLogged.Role.ToString())
                ], "JwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(Anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserViewModel? userLogged)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userLogged != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Email, userLogged.Email),
                    new Claim(ClaimTypes.Role, userLogged.Role.ToString())
                ]));
                await _localStorageService.SetItemEncryptedAsync("userLogged", userLogged);
            }
            else
            {
                claimsPrincipal = Anonymous;
                await _localStorageService.RemoveItemAsync("userLogged");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string?> GetTokenAsync()
        {
            var token = string.Empty;
            try
            {
                var userLogged = await _localStorageService.GetItemDecryptedAsync<UserViewModel>("userLogged");
                if (userLogged != null)
                {
                    token = userLogged.Token;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return token;
        }
    }
}
