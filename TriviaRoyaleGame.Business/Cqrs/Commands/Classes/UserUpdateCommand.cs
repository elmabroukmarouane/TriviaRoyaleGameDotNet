using TriviaRoyaleGame.Business.Cqrs.Commands.Interfaces;
using TriviaRoyaleGame.Business.Helpers;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface;

namespace TriviaRoyaleGame.Business.Cqrs.Commands.Classes
{
    public class UserUpdateCommand : IUserUpdateCommand
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextTriviaRoyaleGame> _unitOfWork;
        #endregion

        #region CONTRUCTOR
        public UserUpdateCommand(IUnitOfWork<DbContextTriviaRoyaleGame> unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region HANDLE
        public async Task<User?> Handle(User entity, bool updateOnLineAuthentication = false)
        {
            if (entity is null) return entity;
            if (!updateOnLineAuthentication) entity = Helper.EncryptPassword(entity);
            await _unitOfWork.GetGenericRepository<User>().UpdateAsync(entity);
            await _unitOfWork.Save();
            return entity;
        }

        public async Task<IList<User>?> Handle(IList<User> entities)
        {
            if (entities is null || !entities.Any()) return entities;
            entities = Helper.EncryptPassword(entities);
            await _unitOfWork.GetGenericRepository<User>().UpdateAsync(entities);
            await _unitOfWork.Save();
            return entities;
        }
        #endregion
    }
}
