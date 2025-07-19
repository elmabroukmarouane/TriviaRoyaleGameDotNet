namespace TriviaRoyaleGame.Infrastructure.Models.Classes
{
    public class Member : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
