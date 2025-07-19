using TriviaRoyaleGame.Business.Cqrs.Commands.Interfaces;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface;

namespace TriviaRoyaleGame.Business.Cqrs.Commands.Classes
{
    public class GenericCreateCommand<TEntity>(IUnitOfWork<DbContextTriviaRoyaleGame> unitOfWork) : IGenericCreateCommand<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextTriviaRoyaleGame> _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region HANDLE
        public async Task<TEntity?> Handle(TEntity entity)
        {
            if (entity is null) return entity;
            await _unitOfWork.GetGenericRepository<TEntity>().CreateAsync(entity);
            await _unitOfWork.Save();
            return entity;
        }

        public async Task<IList<TEntity>?> Handle(IList<TEntity> entities)
        {
            if (entities is null || !entities.Any()) return entities;
            await _unitOfWork.GetGenericRepository<TEntity>().CreateAsync(entities);
            await _unitOfWork.Save();
            return entities;
        }
        #endregion
    }
}
