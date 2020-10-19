using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificReport.Services.Abstraction
{
    public interface IServiceBase<TEntity> where TEntity : IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync<TOrder>(QuerySpecification<TEntity,TOrder> specification);
        Task<TEntity> GetByIdAsync(int entityId);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int entityId);
    }
}
