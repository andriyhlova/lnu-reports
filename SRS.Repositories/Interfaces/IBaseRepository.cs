using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRS.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<Guid> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(Guid id);
        Task<TEntity> Get(Guid id);
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetAll();
    }
}
