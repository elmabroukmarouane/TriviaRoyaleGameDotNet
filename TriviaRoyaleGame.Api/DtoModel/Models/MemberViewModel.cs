using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Api.DtoModel.Models
{
    public class MemberViewModel : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<UserViewModel>? UserViewModels { get; set; }
    }
}
