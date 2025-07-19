using TriviaRoyaleGame.Business.Cqrs.Queries.Interfaces;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface;

namespace TriviaRoyaleGame.Business.Cqrs.Queries.Classes
{
    public class GenericDeleteQuery<TEntity>(IUnitOfWork<DbContextTriviaRoyaleGame> unitOfWork) : IGenericDeleteQuery<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextTriviaRoyaleGame> _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region METHODS
        public async Task<TEntity?> Handle(TEntity entity)
        {
            return await _unitOfWork.GetGenericRepository<TEntity>().DeleteAsync(entity);
        }

        public async Task<IList<TEntity>?> Handle(IList<TEntity> entities)
        {
            return await _unitOfWork.GetGenericRepository<TEntity>().DeleteAsync(entities);
        }
        #endregion
    }
}
