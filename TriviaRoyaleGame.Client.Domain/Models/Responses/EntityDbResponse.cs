using System.Net;

namespace TriviaRoyaleGame.Client.Domain.Models.Responses
{
    public class EntityDbResponse<TEntityViewModel> where TEntityViewModel : Entity
    {
        public bool IsSuccess { get; set; }
        public MessageStatus? MessageStatus { get; set; }
        public TEntityViewModel? Entity { get; set; }
    }
}
