using System.Net;

namespace TriviaRoyaleGame.Client.Domain.Models.Responses
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
