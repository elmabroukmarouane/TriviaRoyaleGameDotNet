﻿using TriviaRoyaleGame.Domain.GenericRepository.Class;
using TriviaRoyaleGame.Domain.GenericRepository.Interface;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace TriviaRoyaleGame.UnitOfWork.UnitOfWork.Class
{
    public class UnitOfWork<TDbContext>(TDbContext dbContext) : IUnitOfWork<TDbContext> where TDbContext : DbContext 
    {
        #region ATTRIBUTES
        public TDbContext DbContext { get; } = dbContext ?? throw new ArgumentException(null, nameof(dbContext));
        private bool DisposedValue;
        private Dictionary<Type, object>? Repositories;
        #endregion

        #region GET REPOSITORY
        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : Entity
        {
            Repositories ??= []; 
            var type = typeof(TEntity);
            if(!Repositories.ContainsKey(type))
                Repositories[type] = new GenericRepository<TEntity>(DbContext);
            return (IGenericRepository<TEntity>)Repositories[type];
        }
        #endregion

        #region SAVE CHANGES
        public async Task<int> Save()
        {
            return await DbContext.SaveChangesAsync();
        }
        #endregion

        #region FREE MEMORY BY DISPOSING (IF YOU HAVE A NON-MANAGED CODE PLEASE UNCOMMENT DESTRUCTOR SECTION COMMENTED AND FILL NON-MANAGED SECTION IN Dispose(bool disposing) METHOD)
        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                    if (Repositories != null)
                        Repositories.Clear();
                    DbContext.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                DisposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~UnitOfWork()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
