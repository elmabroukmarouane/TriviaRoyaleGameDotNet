using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using TriviaRoyaleGame.Client.Business.Extensions.LocalStorage;
using TriviaRoyaleGame.Client.Business.Extensions.Logging;
using TriviaRoyaleGame.Client.Business.Helpers;
using TriviaRoyaleGame.Client.Business.Providers.Interfaces;
using TriviaRoyaleGame.Client.Business.Services.CryptoService.Interface;
using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Responses;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

namespace TriviaRoyaleGame.Client.Business.Providers.Classes
{
    public class ClientAppAuthenticationStateProvider(HttpClient httpClient, BaseSettingsApp? baseSettingsApp, JwtAppSettings? jwtAppSettings, ISourceAppProvider? SourceAppProvider, ILocalStorageService? localStorageService, ICryptoService cryptoService) : AuthenticationStateProvider
    {
        protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentException(null, nameof(httpClient));
        protected readonly BaseSettingsApp? _baseSettingsApp = baseSettingsApp ?? throw new ArgumentException(null, nameof(baseSettingsApp));
        protected readonly JwtAppSettings? _jwtAppSettings = jwtAppSettings ?? throw new ArgumentException(null, nameof(jwtAppSettings));
        protected readonly ISourceAppProvider? _SourceAppProvider = SourceAppProvider ?? throw new ArgumentException(null, nameof(SourceAppProvider));
        private readonly ILocalStorageService _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        private readonly ICryptoService _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        private ClaimsPrincipal Anonymous { get; set; } = new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemDecryptedAsync<TokenResponse?>("token", _baseSettingsApp?.OpenerString, _baseSettingsApp?.BaseUrlApiWebHttp + "crypto/decrypt"/*, token.Token*/, _cryptoService);
                if (token == null)
                {
                    return await Task.FromResult(new AuthenticationState(Anonymous));
                }
                var userLogged = Helper.DecryptAndDeserializeUserViewModel(token.Token);
                if (userLogged == null)
                {
                    return await Task.FromResult(new AuthenticationState(Anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("user", JsonSerializer.Serialize(userLogged)),
                    new Claim(ClaimTypes.Role, Helper.GetRoleConnectedUser(token.Token) ?? string.Empty)
                ], "JwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Providers.Classes.ClientAppAuthenticationStateProvider - GetAuthenticationStateAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                return await Task.FromResult(new AuthenticationState(Anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserViewModel? userLogged, string? token = null)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal;
                if (userLogged != null)
                {
                    IList<Claim> claims = [
                        new Claim("user", JsonSerializer.Serialize(userLogged))
                    ];
                    if (token is not null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, Helper.GetRoleConnectedUser(token) ?? string.Empty));
                    }
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));
                }
                else
                {
                    claimsPrincipal = Anonymous;
                    await _localStorageService.RemoveItemAsync("token");
                }
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Providers.Classes.ClientAppAuthenticationStateProvider - UpdateAuthenticationState()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
