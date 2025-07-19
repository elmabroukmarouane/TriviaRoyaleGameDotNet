using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Api.DtoModel.Models
{
    public class QuestionViewModel : Entity
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Choices { get; set; } = [];
        public int CorrectChoiceIndex { get; set; }
    }
}
