using TriviaRoyaleGame.Business.Cqrs.Queries.Interfaces;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface;

namespace TriviaRoyaleGame.Business.Cqrs.Queries.Classes
{
    public class GenericTruncateQuery<TEntity>(IUnitOfWork<DbContextTriviaRoyaleGame> unitOfWork) : IGenericTruncateQuery<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextTriviaRoyaleGame> _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region METHODS
        public async Task Handle(string sql, bool isSqlite)
        {
            await _unitOfWork.GetGenericRepository<TEntity>().TruncateAsync(sql, isSqlite);
        }
        #endregion
    }
}
