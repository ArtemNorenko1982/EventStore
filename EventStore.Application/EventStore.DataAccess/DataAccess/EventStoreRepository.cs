using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EventStore.Data.Interfaces;
using EventStore.DataAccess.EF;
using EventStore.DataContracts;
using EventStore.DataContracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventStore.DataAccess.DataAccess
{
    public class EventStoreRepository<TModel, TEntity> : IEventStoreRepository<TModel, TEntity>
        where TModel : class, IBaseModel
        where TEntity : class, IBaseEntity
    {
        private readonly EventStoreContext _context;
        private readonly IMapper _mapper;

        public EventStoreRepository(EventStoreContext eventStoreContext, IMapper mapper)
        {
            _context = eventStoreContext;
            _mapper = mapper;
        }

        public async Task<TModel> AddAsync(TModel model)
        {
            var result = 0;
            var entity = _mapper.Map<TEntity>(model);
            var queryResult = new List<TEntity> { entity };

            using (_context)
            {
                await _context.Set<TEntity>().AddAsync(entity);
                result = await _context.SaveChangesAsync();
            }

            return result > 0 ? _mapper.Map<TModel>(queryResult.FirstOrDefault()) : null;
        }

        public async Task<List<TModel>> AddAsync(IEnumerable<TModel> models)
        {
            var result = 0;
            var entities = _mapper.Map<IEnumerable<TEntity>>(models);
            var addedEntities = new List<TEntity>();
            using (_context)
            {
                foreach (var entity in entities)
                {
                    var addedEntity = _context.Set<TEntity>().Add(entity);
                    _context.SaveChanges();
                    var model = _context.Set<TEntity>().AsQueryable()
                        .Single(_ => _.Id == addedEntity.Entity.Id);
                    addedEntities.Add(model);
                }
            }

            return _mapper.Map<List<TModel>>(addedEntities);
        }

        public async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunction = null, bool asNoTracking = false)
        {
            using (_context)
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (includeFunction != null)
                {
                    query = includeFunction(query);
                }

                var result = await query.ToListAsync();
                return _mapper.Map<IEnumerable<TModel>>(result);
            }
        }

        public async Task<TModel> GetSingleAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunction = null, bool asNoTracking = false)
        {
            var result = (await GetAsync(predicate, includeFunction, asNoTracking)).FirstOrDefault();
            return _mapper.Map<TModel>(result);
        }

        public async Task<TModel> UpdateAsync(TEntity entity)
        {
            TEntity value;
            var result = 0;
            using (_context)
            {
                _context.Set<TEntity>().Attach(entity);
                result = await _context.SaveChangesAsync();
                value = await _context.FindAsync<TEntity>(entity.Id);
            }

            return result > 0 ? _mapper.Map<TModel>(value) : null;
        }

        public async Task<bool> UpdateAsync(IEnumerable<TEntity> entities)
        {
            var result = 0;
            using (_context)
            {
                foreach (var entity in entities)
                {
                    _context.Set<TEntity>().Attach(entity);
                }

                result = await _context.SaveChangesAsync();
            }

            return result > 0;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var result = 0;
            using (_context)
            {
                var item = await _context.Set<TEntity>().FindAsync(id);
                _context.Set<TEntity>().Remove(item);
                result = await _context.SaveChangesAsync();
            }

            return result > 0;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            var result = 0;
            using (_context)
            {
                _context.Set<TEntity>().Remove(entity);
                result = await _context.SaveChangesAsync();
            }

            return result > 0;
        }

        public Task<bool> RemoveAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException("This functionality in development.");
        }
    }
}
