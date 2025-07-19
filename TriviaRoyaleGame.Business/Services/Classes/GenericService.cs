﻿using TriviaRoyaleGame.Business.Cqrs.Commands.Interfaces;
using TriviaRoyaleGame.Business.Cqrs.Queries.Interfaces;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using System.Linq.Expressions;

namespace TriviaRoyaleGame.Business.Services.Classes
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IGenericCreateCommand<TEntity> _genericCreateCommand;
        protected readonly IGenericUpdateCommand<TEntity> _genericUpdateCommand;
        protected readonly IGenericGetEntitiesQuery<TEntity> _genericGetEntitiesQuery;
        protected readonly IGenericDeleteQuery<TEntity> _genericDeleteQuery;
        protected readonly IGenericTruncateQuery<TEntity> _genericTruncateQuery;
        #endregion

        #region CONSTRUCTOR
        public GenericService(
            IGenericCreateCommand<TEntity> genericCreateCommand, 
            IGenericUpdateCommand<TEntity> genericUpdateCommand, 
            IGenericGetEntitiesQuery<TEntity> genericGetEntitiesQuery, 
            IGenericDeleteQuery<TEntity> genericDeleteQuery,
            IGenericTruncateQuery<TEntity> genericTruncateQuery)
        {
            _genericCreateCommand = genericCreateCommand ?? throw new ArgumentException(null, nameof(genericCreateCommand));
            _genericUpdateCommand = genericUpdateCommand ?? throw new ArgumentException(null, nameof(genericUpdateCommand));
            _genericGetEntitiesQuery = genericGetEntitiesQuery ?? throw new ArgumentException(null, nameof(genericGetEntitiesQuery));
            _genericDeleteQuery = genericDeleteQuery ?? throw new ArgumentException(null, nameof(genericDeleteQuery));
            _genericTruncateQuery = genericTruncateQuery ?? throw new ArgumentException(null, nameof(genericTruncateQuery));
        }
        #endregion

        #region CREATE
        public async Task<TEntity?> CreateAsync(TEntity entity)
        {
            return await _genericCreateCommand.Handle(entity);
        }

        public async Task<IList<TEntity>?> CreateAsync(IList<TEntity> entities)
        {
            return await _genericCreateCommand.Handle(entities);
        }
        #endregion

        #region DELETE
        public async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            return await _genericDeleteQuery.Handle(entity);
        }

        public async Task<IList<TEntity>?> DeleteAsync(IList<TEntity> entities)
        {
            return await _genericDeleteQuery.Handle(entities);
        }
        #endregion

        #region READ
        public IQueryable<TEntity> GetEntitiesAsync(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null, string? includes = null, string splitChar = ",", bool disableTracking = true, int take = 0, int offset = 0, bool inDatabase = false)
        {
            return _genericGetEntitiesQuery.Handle(expression, orberBy, includes, splitChar, disableTracking, take, offset, inDatabase);
        }
        public IQueryable<TEntity?> GetEntitiesAsync()
        {
            return _genericGetEntitiesQuery.Handle();
        }

        public async Task<TEntity?> GetEntitiesAsync(TEntity entity)
        {
            return await _genericGetEntitiesQuery.Handle(entity);
        }
        #endregion

        #region UPDATE
        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            return await _genericUpdateCommand.Handle(entity);
        }

        public async Task<IList<TEntity>?> UpdateAsync(IList<TEntity> entities)
        {
            return await _genericUpdateCommand.Handle(entities);
        }
        #endregion

        #region TRUNCATE
        public async Task TruncateAsync(string sql, bool isSqlite)
        {
            await _genericTruncateQuery.Handle(sql, isSqlite);
        }
        #endregion
    }
}
