using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Context;
using SRS.Repositories.Interfaces;
using SRS.Repositories.Utilities;

namespace SRS.Repositories.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            AddRelatedEntities(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var existingEntity = await GetAsync(entity.Id);

            if (existingEntity == null)
            {
                return existingEntity;
            }

            UpdateRelatedEntities(existingEntity, entity);
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await GetAsync(id);

            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual Task<TEntity> GetAsync(int id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual Task<TEntity> GetAsync(int id, ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification)
                .ToListAsync();
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual Task<int> CountAsync()
        {
            return _context.Set<TEntity>().CountAsync();
        }

        public virtual Task<int> CountAsync(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification)
                .CountAsync();
        }

        protected virtual void AddRelatedEntities(TEntity entity)
        {
        }

        protected virtual void UpdateRelatedEntities(TEntity existingEntity, TEntity newEntity)
        {
        }

        protected void AddCollection<TRelatedEntity>(ICollection<TRelatedEntity> newCollection)
            where TRelatedEntity : BaseEntity
        {
            if (newCollection != null)
            {
                foreach (var item in newCollection)
                {
                    _context.Entry(item).State = EntityState.Unchanged;
                }
            }
        }

        protected void UpdateCollection<TRelatedEntity>(ICollection<TRelatedEntity> existingCollection, ICollection<TRelatedEntity> newCollection)
            where TRelatedEntity : BaseEntity
        {
            var toDelete = existingCollection.Where(x => !newCollection.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toDelete)
            {
                existingCollection.Remove(item);
            }

            var toAdd = newCollection.Where(x => !existingCollection.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toAdd)
            {
                _context.Entry(item).State = EntityState.Unchanged;
                existingCollection.Add(item);
            }
        }
    }
}
