using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Business.Cqrs.Queries.Interfaces
{
    public interface IGenericTruncateQuery<TEntity> where TEntity : Entity
    {
        Task Handle(string sql, bool isSqlite);
    }
}
