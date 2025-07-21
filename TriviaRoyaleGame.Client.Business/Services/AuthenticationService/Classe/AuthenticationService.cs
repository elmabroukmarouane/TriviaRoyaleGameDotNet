using System.Net.Http.Json;
using TriviaRoyaleGame.Client.Business.Helpers;
using TriviaRoyaleGame.Client.Business.Services.AuthenticationService.Interface;
using TriviaRoyaleGame.Client.Domain.Models;

namespace TriviaRoyaleGame.Client.Business.Services.AuthenticationService.Classe;
public class AuthenticationService(HttpClient httpClient) : IAuthenticationService
{
    #region ATTRIBUTES
    protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentException(null, nameof(httpClient));
    //private readonly IRedisService _redisService;
    //private SessionState _sessionState;
    #endregion

    #region METHODS
    public async Task<UserViewModel?> Authenticate(UserViewModel UserLogin, string uri)
    {
        var UserResponseHttp = await _httpClient.PostAsJsonAsync(uri, UserLogin);
        //var UserResponse = await UserResponseHttp.Content.ReadFromJsonAsync<UserViewModel>();
        //UserResponse!.StatusCode = UserResponseHttp.StatusCode;
        var token  = await UserResponseHttp.Content.ReadFromJsonAsync<object>();
        if (token == null) return null;
        var userLogged = Helper.DecryptAndDeserializeUserViewModel(token.ToString());
        return userLogged;
    }

    //public async Task<string> GetSessionId(string password, string fullName)
    //{
    //    var sessionIdByte = await Helper.EncryptAsync(password, fullName);
    //    return sessionIdByte;
    //}

    //public async Task<string> GetReverseSessionId(string sessionId, string fullName)
    //{
    //    var reverseSessionId = await Helper.DecryptAsync(sessionId, fullName);
    //    return reverseSessionId;
    //}

    //public async Task<UserLogged> GetSession(string uri, string userSessionId)
    //{
    //    var userSessionActual = _sessionState.Users.Where(u => u.UserSessionId == userSessionId).FirstOrDefault();
    //    if(userSessionActual != null)
    //    {
    //        return userSessionActual;
    //    }
    //    else
    //    {
    //        var response = await _httpClient.GetAsync(uri + userSessionActual);
    //        var userLogged = await response.Content.ReadFromJsonAsync<UserLogged>();
    //        if(userLogged.UserId != 0)
    //        {
    //            var userSessionToDelete = _sessionState.Users.Where(u => u.UserSessionId == userLogged.UserSessionId).FirstOrDefault();
    //            if (userSessionToDelete is not null)
    //            {
    //                _sessionState.Users.Remove(userSessionToDelete);
    //            }
    //            _sessionState.Users.Add(userLogged);
    //        }
    //        return userLogged;
    //    }
    //}

    public async Task Logout(string uri, string token)
    {
        SetTokenToHeader(token);
        await _httpClient.GetAsync(uri);
    }
    #endregion

    #region ADDTOKEN
    private void SetTokenToHeader(string? token)
    {
        if (token != null)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }
    }
    #endregion
}
