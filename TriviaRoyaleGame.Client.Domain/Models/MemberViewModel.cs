namespace TriviaRoyaleGame.Client.Domain.Models
{
    public class MemberViewModel : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<UserViewModel>? UserViewModels { get; set; }
    }
}
