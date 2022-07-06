using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRS.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetAsync(int id);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> GetAllAsync();
    }
}
