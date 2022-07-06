using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Repositories.Context;
using SRS.Repositories.Interfaces;

namespace SRS.Repositories.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
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

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }
    }
}
