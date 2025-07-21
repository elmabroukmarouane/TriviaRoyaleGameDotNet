using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Responses;

namespace TriviaRoyaleGame.Client.Business.Services.AuthenticationService.Interface
{
    public interface IAuthenticationService
    {
        Task<TokenResponse?> Authenticate(UserViewModel UserLogin, string uri);
        Task Logout(string uri, string token);
        //Task<string> GetSessionId(string password, string fullName);
        //Task<string> GetReverseSessionId(string sessionId, string fullName);
    }
}
