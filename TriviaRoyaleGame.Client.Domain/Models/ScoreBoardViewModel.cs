namespace TriviaRoyaleGame.Client.Domain.Models
{
    public class ScoreBoardViewModel : Entity
    {
        public int UserId { get; set; }
        public int Score { get; set; }
        public UserViewModel? UserViewModel { get; set; }
    }
}
