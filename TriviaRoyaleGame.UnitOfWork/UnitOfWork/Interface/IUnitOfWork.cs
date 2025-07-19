using TriviaRoyaleGame.Domain.GenericRepository.Interface;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface
{
    public interface IUnitOfWork<TDbContext> : IDisposable 
        where TDbContext : DbContext 
    {
        TDbContext DbContext { get; }
        IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : Entity;
        Task<int> Save();
    }
}
