using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventStore.Data.Interfaces;
using EventStore.DataContracts.Interfaces;

namespace EventStore.DataContracts
{
    public interface IEventStoreRepository<TModel, TEntity>
        where TModel : class, IBaseModel
        where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// Add single entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Entity</returns>
        Task<TModel> AddAsync(TModel model);

        Task<List<TModel>> AddAsync(IEnumerable<TModel> models);

        Task<IEnumerable<TModel>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunction = null, bool asNoTracking = false);

        Task<TModel> GetSingleAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunction = null, bool asNoTracking = false);

        Task<TModel> UpdateAsync(TEntity entity);

        Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

        Task<bool> RemoveAsync(string id);

        Task<bool> RemoveAsync(TEntity entity);

        Task<bool> RemoveAsync(IEnumerable<TEntity> entities);

        void CleanContext();
    }
}
