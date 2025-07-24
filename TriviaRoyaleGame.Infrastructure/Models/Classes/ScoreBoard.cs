namespace TriviaRoyaleGame.Infrastructure.Models.Classes;
public class ScoreBoard : Entity
{
    public int UserId { get; set; }
    public int Score { get; set; }
    public virtual User? User { get; set; }
}