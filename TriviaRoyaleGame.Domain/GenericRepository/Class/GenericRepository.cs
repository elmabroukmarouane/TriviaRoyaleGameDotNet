using TriviaRoyaleGame.Domain.GenericRepository.Interface;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TriviaRoyaleGame.Domain.GenericRepository.Class
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region CONSTRUCTOR
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(null, nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region CREATE
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            await _dbSet.AddAsync(entity: entity);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            // Retrieve navigation properties
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            var navigationProperties = entityType?.GetNavigations();

            if (navigationProperties is not null)
            {
                foreach (var navigation in navigationProperties)
                {
                    var navigationEntry = _dbContext.Entry(entity).Navigation(navigation.Name);

                    if (!navigationEntry.IsLoaded)
                    {
                        if (navigation.IsCollection)
                        {
                            await _dbContext.Entry(entity).Collection(navigation.Name).LoadAsync();
                        }
                        else
                        {
                            await _dbContext.Entry(entity).Reference(navigation.Name).LoadAsync();
                        }
                    }

                    //// Access the related entity if needed
                    //var relatedEntity = referenceEntry.CurrentValue;
                    //if (relatedEntity != null)
                    //{
                    //    // Do something with the related entity if necessary
                    //}
                }
            }
            return entity;
        }

        public virtual async Task<IList<TEntity>> CreateAsync(IList<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities: entities);
            await _dbContext.SaveChangesAsync();
            foreach (var entity in entities)
            {
                // Retrieve navigation properties
                var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
                var navigationProperties = entityType?.GetNavigations();

                if (navigationProperties is not null)
                {
                    foreach (var navigation in navigationProperties)
                    {
                        var navigationEntry = _dbContext.Entry(entity).Navigation(navigation.Name);

                        if (!navigationEntry.IsLoaded)
                        {
                            if (navigation.IsCollection)
                            {
                                await _dbContext.Entry(entity).Collection(navigation.Name).LoadAsync();
                            }
                            else
                            {
                                await _dbContext.Entry(entity).Reference(navigation.Name).LoadAsync();
                            }
                        }

                        //// Access the related entity if needed
                        //var relatedEntity = referenceEntry.CurrentValue;
                        //if (relatedEntity != null)
                        //{
                        //    // Do something with the related entity if necessary
                        //}
                    }
                }
            }
            return entities;
        }
        #endregion

        #region DELETE
        public virtual async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            var entityToDelete = await _dbSet.FindAsync(entity.Id);
            if(entityToDelete != null)
            {
                _dbSet.Remove(entity: entityToDelete);
                await _dbContext.SaveChangesAsync();
            }
            return entityToDelete;
        }

        public virtual async Task<IList<TEntity>?> DeleteAsync(IList<TEntity> entities)
        {
            _dbSet.RemoveRange(entities: entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
        #endregion

        #region READ
        public virtual IQueryable<TEntity> GetEntitiesAsync(
            Expression<Func<TEntity, bool>>? expression = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null, 
            string? includes = null,
            string splitChar = ",", 
            bool disableTracking = true, 
            int take = 0, 
            int offset = 0)
        {
            var queryableEntity = (IQueryable<TEntity>)_dbSet;

            if (expression != null)
            {
                queryableEntity = queryableEntity.Where(expression);
            }

            if (orberBy != null)
            {
                queryableEntity = orberBy(queryableEntity);
            }

            if (includes != null)
            {
                foreach (var include in includes.Split(splitChar, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryableEntity = queryableEntity.Include(include);
                }
            }

            if (disableTracking)
            {
                queryableEntity = queryableEntity.AsNoTracking();
            }

            if (take > 0)
            {
                queryableEntity = queryableEntity.Take(take);
            }

            if (offset > 0)
            {
                queryableEntity = queryableEntity.Skip(offset);
            }

            return queryableEntity;
        }

        public virtual async Task<TEntity?> GetEntitiesAsync(TEntity entity) => await _dbSet.FindAsync(entity.Id);
        #endregion

        #region UPDATE
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();

            // Retrieve navigation properties
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            var navigationProperties = entityType?.GetNavigations();

            if (navigationProperties is not null)
            {
                foreach (var navigation in navigationProperties)
                {
                    var navigationEntry = _dbContext.Entry(entity).Navigation(navigation.Name);

                    if (!navigationEntry.IsLoaded)
                    {
                        if (navigation.IsCollection)
                        {
                            await _dbContext.Entry(entity).Collection(navigation.Name).LoadAsync();
                        }
                        else
                        {
                            await _dbContext.Entry(entity).Reference(navigation.Name).LoadAsync();
                        }
                    }

                    //// Access the related entity if needed
                    //var relatedEntity = referenceEntry.CurrentValue;
                    //if (relatedEntity != null)
                    //{
                    //    // Do something with the related entity if necessary
                    //}
                }
            }

            return entity;
        }

        public async Task<IList<TEntity>> UpdateAsync(IList<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            foreach (var entity in entities)
            {
                // Retrieve navigation properties
                var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
                var navigationProperties = entityType?.GetNavigations();

                if (navigationProperties is not null)
                {
                    foreach (var navigation in navigationProperties)
                    {
                        var navigationEntry = _dbContext.Entry(entity).Navigation(navigation.Name);

                        if (!navigationEntry.IsLoaded)
                        {
                            if (navigation.IsCollection)
                            {
                                await _dbContext.Entry(entity).Collection(navigation.Name).LoadAsync();
                            }
                            else
                            {
                                await _dbContext.Entry(entity).Reference(navigation.Name).LoadAsync();
                            }
                        }

                        //// Access the related entity if needed
                        //var relatedEntity = referenceEntry.CurrentValue;
                        //if (relatedEntity != null)
                        //{
                        //    // Do something with the related entity if necessary
                        //}
                    }
                }
            }
            return entities;
        }
        #endregion

        #region TRUNCATE
        public async Task TruncateAsync(string sql, bool isSqlite)
        {
            var dbSetProperty = _dbContext.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(DbSet<TEntity>));
            if (dbSetProperty != null)
            {
                sql = sql.Replace("tableName", dbSetProperty?.Name);
                await _dbContext.Database.ExecuteSqlRawAsync(sql);
                if(isSqlite)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("VACUUM;");
                }
            }
        }
        #endregion
    }
}
