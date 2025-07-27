namespace TriviaRoyaleGame.Infrastructure.Models.Classes;
public class Question : Entity
{
    public int CategoryId { get; set; }
    public string Text { get; set; } = string.Empty;
    public List<string> Choices { get; set; } = [];
    public int CorrectChoiceIndex { get; set; }
    public virtual Category? Category { get; set; }
}
