namespace TriviaRoyaleGame.Infrastructure.Models.Classes
{
    public class Category : Entity
    {
        public string? Name { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
    }
}
