namespace TriviaRoyaleGame.Client.Domain.Models
{
    public class CategoryViewModel : Entity
    {
        public string? Name { get; set; }
        public virtual ICollection<QuestionViewModel>? QuestionViewModels { get; set; }
    }
}
