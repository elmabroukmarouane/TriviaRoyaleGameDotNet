namespace TriviaRoyaleGame.Infrastructure.Models.Classes
{
    /// <summary>
    /// Level: Info, Warning, Error
    /// Message: Log message
    /// Source: "Web", "Android", "Windows Desktop", "Linux Desktop", "iOS"
    /// </summary>
    public class ClientAppLog : Entity
    {
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
    }
}
