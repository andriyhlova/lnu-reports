using SRS.Domain.Entities;
using SRS.Repositories.Context;
using SRS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRS.Repositories.Implementations
{
    public class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var existingEntity = await Get(entity.Id);

            if (existingEntity == null)
            {
                return existingEntity;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var existingEntity = await Get(id);

            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual Task<TEntity> Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public virtual Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return _context.Set<TEntity>().ToListAsync();
        }
    }
}
