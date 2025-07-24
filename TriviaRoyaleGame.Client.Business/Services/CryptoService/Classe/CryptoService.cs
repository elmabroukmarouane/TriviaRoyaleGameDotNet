using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using TriviaRoyaleGame.Client.Business.Extensions.Logging;
using TriviaRoyaleGame.Client.Business.Providers.Interfaces;
using TriviaRoyaleGame.Client.Business.Services.CryptoService.Interface;
using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

namespace TriviaRoyaleGame.Client.Business.Services.CryptoService.Classe;
public class CryptoService(HttpClient httpClient, BaseSettingsApp? baseSettingsApp, ISourceAppProvider? SourceAppProvider) : ICryptoService
{
    #region ATTRIBUTES
    protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentException(null, nameof(httpClient));
    protected readonly BaseSettingsApp? _baseSettingsApp = baseSettingsApp ?? throw new ArgumentException(null, nameof(baseSettingsApp));
    protected readonly ISourceAppProvider? _SourceAppProvider = SourceAppProvider ?? throw new ArgumentException(null, nameof(SourceAppProvider));
    #endregion

    #region METHODS
    public async Task<string?> EncryptAsync(CryptoPayloadViewModel cryptoPayloadViewModel, string? uri/*, string? token*/)
    {
        try
        {
            //SetTokenToHeader(token);
            var cryptoResponseHttp = await _httpClient.PostAsJsonAsync(uri, cryptoPayloadViewModel);
            var cryptoResponse = new CryptoResponse();
            if (cryptoResponseHttp.IsSuccessStatusCode)
            {
                cryptoResponse = await cryptoResponseHttp.Content.ReadFromJsonAsync<CryptoResponse>();
            }
            return cryptoResponse?.Response;
        }
        catch (Exception ex)
        {
            var log = LoggingMessaging.LoggingMessageError(
                nameSpaceName: "TriviaRoyaleGame.Client.Business",
                statusCodeInt: (int)HttpStatusCode.InternalServerError,
                statusCode: HttpStatusCode.InternalServerError.ToString(),
                actionName: "Services.Class.AuthenticationService - Authenticate()",
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

    public async Task<string?> DecryptAsync(CryptoPayloadViewModel cryptoPayloadViewModel, string? uri/*, string? token*/)
    {
        try
        {
            //SetTokenToHeader(token);
            var cryptoResponseHttp = await _httpClient.PostAsJsonAsync(uri, cryptoPayloadViewModel);
            var cryptoResponse = new CryptoResponse();
            if (cryptoResponseHttp.IsSuccessStatusCode)
            {
                cryptoResponse = await cryptoResponseHttp.Content.ReadFromJsonAsync<CryptoResponse>();
            }
            return cryptoResponse?.Response;
        }
        catch (Exception ex)
        {
            var log = LoggingMessaging.LoggingMessageError(
                nameSpaceName: "TriviaRoyaleGame.Client.Business",
                statusCodeInt: (int)HttpStatusCode.InternalServerError,
                statusCode: HttpStatusCode.InternalServerError.ToString(),
                actionName: "Services.Class.AuthenticationService - Logout()",
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
    #endregion

    #region ADDTOKEN
    //private void SetTokenToHeader(string? token)
    //{
    //    if (token != null)
    //    {
    //        if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
    //        {
    //            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    //        }
    //    }
    //}
    #endregion
}

internal class CryptoResponse
{
    public string? Response { get; set; }
}
