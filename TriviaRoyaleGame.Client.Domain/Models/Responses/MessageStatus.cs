using System.Net;

namespace TriviaRoyaleGame.Client.Domain.Models.Responses
{
    public class MessageStatus
    {
        public HttpStatusCode? StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
