using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Api.DtoModel.Models
{
    public class CategoryViewModel : Entity
    {
        public string? Name { get; set; }
        public virtual ICollection<QuestionViewModel>? QuestionViewModels { get; set; }
    }
}
