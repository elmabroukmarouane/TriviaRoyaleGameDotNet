using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.Extensions.Caching.Memory;

namespace TriviaRoyaleGame.Business.Helpers
{
    public class HelperCache<TEntity> where TEntity : Entity
    {
        private static readonly string CacheKey = $"{typeof(TEntity).Name}Cache";
        public static void AddCache(TEntity entity, IMemoryCache cache)
        {
            if (entity is null) return;
            var cachedListData = cache.GetOrCreate(CacheKey, entry => new List<TEntity>(), new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove,
                Size = 1
            });
            if (cachedListData is not null)
            {
                var existingEntity = cachedListData.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (existingEntity is not null)
                {
                    cachedListData.Remove(existingEntity);
                }
                cachedListData.Add(entity);
                cache.Set(CacheKey, cachedListData, new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.NeverRemove,
                    Size = cachedListData?.Count
                });
            }
        }

        public static void AddCache(IList<TEntity> entities, IMemoryCache cache)
        {
            if (entities is null || !entities.Any()) return;
            var cachedListData = cache.GetOrCreate(CacheKey, entry => new List<TEntity>(), new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove,
                Size = 1
            });
            if (cachedListData is not null)
            {
                foreach (var entity in entities)
                {
                    var existingEntity = cachedListData.Where(x => x.Id == entity.Id).FirstOrDefault();
                    if (existingEntity is not null)
                    {
                        cachedListData.Remove(existingEntity);
                    }
                    cachedListData.Add(entity);
                }
                cache.Set(CacheKey, cachedListData, new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.NeverRemove,
                    Size = cachedListData?.Count
                });
            }
        }

        public static void DeleteCache(TEntity entity, IMemoryCache cache)
        {
            if (entity is null) return;
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            if (cache.TryGetValue(CacheKey, out IList<TEntity> cachedListData))
            {
                var itemToRemove = cachedListData?.FirstOrDefault(x => x.Id == entity.Id);
                if (itemToRemove is not null)
                {
                    cachedListData?.Remove(itemToRemove);
                    cache.Set(CacheKey, cachedListData, new MemoryCacheEntryOptions
                    {
                        Priority = CacheItemPriority.NeverRemove,
                        Size = cachedListData?.Count
                    });
                }
            }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
        }

        public static void DeleteCache(IList<TEntity> entities, IMemoryCache cache)
        {
            if (entities is null || !entities.Any()) return;
            
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            if (cache.TryGetValue(CacheKey, out IList<TEntity> cachedListData))
            {
                if (cachedListData is not null)
                {
                    foreach (var entity in entities)
                    {
                        var itemToRemove = cachedListData.FirstOrDefault(x => x.Id == entity.Id);
                        if (itemToRemove is not null)
                        {
                            cachedListData.Remove(itemToRemove);
                        }
                    }
                    cache.Set(CacheKey, cachedListData, new MemoryCacheEntryOptions
                    {
                        Priority = CacheItemPriority.NeverRemove,
                        Size = cachedListData?.Count
                    });
                }
            }
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
        }
    }
}
