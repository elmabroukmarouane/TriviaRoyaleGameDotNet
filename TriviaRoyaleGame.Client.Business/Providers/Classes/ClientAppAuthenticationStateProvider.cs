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
using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Responses;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

namespace TriviaRoyaleGame.Client.Business.Providers.Classes
{
    public class ClientAppAuthenticationStateProvider(HttpClient httpClient, BaseSettingsApp? baseSettingsApp, JwtAppSettings? jwtAppSettings, ISourceAppProvider? SourceAppProvider, ILocalStorageService? localStorageService) : AuthenticationStateProvider
    {
        protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentException(null, nameof(httpClient));
        protected readonly BaseSettingsApp? _baseSettingsApp = baseSettingsApp ?? throw new ArgumentException(null, nameof(baseSettingsApp));
        protected readonly JwtAppSettings? _jwtAppSettings = jwtAppSettings ?? throw new ArgumentException(null, nameof(jwtAppSettings));
        protected readonly ISourceAppProvider? _SourceAppProvider = SourceAppProvider ?? throw new ArgumentException(null, nameof(SourceAppProvider));
        private readonly ILocalStorageService _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        private ClaimsPrincipal Anonymous { get; set; } = new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemDecryptedAsync<TokenResponse?>("token");
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
                    new Claim(ClaimTypes.Email, userLogged.Email),
                    new Claim(ClaimTypes.Role, userLogged.Role.ToString())
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

        public async Task UpdateAuthenticationState(UserViewModel? userLogged)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal;
                if (userLogged != null)
                {
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.Email, userLogged.Email),
                        new Claim(ClaimTypes.Role, userLogged.Role.ToString())
                    ], "JwtAuth"));
                    //var token = Helper.CreateToken(userLogged.Email, userLogged.Password, _jwtAppSettings);
                    //var tokenResponseString = JsonSerializer.Serialize(new TokenResponse()
                    //{
                    //    StatusCode = HttpStatusCode.OK,
                    //    Token = token
                    //});
                    //await _localStorageService.SetItemEncryptedAsync("token", tokenResponseString);
                }
                else
                {
                    claimsPrincipal = Anonymous;
                    //await _localStorageService.RemoveItemAsync("token");
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
