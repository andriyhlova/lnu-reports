using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRS.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<bool> Delete(int id);

        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> Get(int id);

        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> GetAll();
    }
}
