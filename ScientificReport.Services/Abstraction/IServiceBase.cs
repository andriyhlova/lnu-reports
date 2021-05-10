using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificReport.Services.Abstraction
{
    public interface IServiceBase<TEntity,T> where TEntity : IBaseEntity<T>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync<TOrder>(QuerySpecification<TEntity,TOrder> specification);
        Task<TEntity> GetByIdAsync(T entityId);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(T entityId);
    }
}
