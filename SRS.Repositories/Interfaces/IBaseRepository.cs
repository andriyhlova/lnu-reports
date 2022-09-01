using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;

namespace SRS.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetAsync(int id);

        Task<TEntity> GetAsync(int id, ISpecification<TEntity> specification);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> GetAsync(ISpecification<TEntity> specification);

        Task<List<TEntity>> GetAllAsync();

        Task<int> CountAsync();

        Task<int> CountAsync(ISpecification<TEntity> specification);
    }
}
