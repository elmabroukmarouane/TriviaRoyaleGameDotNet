using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Business.Cqrs.Commands.Interfaces
{
    public interface IUserCreateCommand
    {
        Task<User?> Handle(User entity);
        Task<IList<User>?> Handle(IList<User> entities);
    }
}
