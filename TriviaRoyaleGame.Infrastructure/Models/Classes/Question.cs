namespace TriviaRoyaleGame.Infrastructure.Models.Classes;
public class Question : Entity
{
    public string Text { get; set; } = string.Empty;
    public List<string> Choices { get; set; } = [];
    public int CorrectChoiceIndex { get; set; }
}
