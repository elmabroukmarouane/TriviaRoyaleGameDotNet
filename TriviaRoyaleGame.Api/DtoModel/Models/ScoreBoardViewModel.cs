using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Api.DtoModel.Models
{
    public class ScoreBoardViewModel : Entity
    {
        public int UserId { get; set; }
        public int Score { get; set; }
        public UserViewModel? UserViewModel { get; set; }
    }
}
