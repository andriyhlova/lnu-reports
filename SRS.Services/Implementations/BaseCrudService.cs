using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Services.Implementations
{
    public class BaseCrudService<TEntity, TModel> : BaseService<TEntity>, IBaseCrudService<TModel>
        where TModel : BaseModel
        where TEntity : BaseEntity
    {
        public BaseCrudService(IBaseRepository<TEntity> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public virtual async Task<int> AddAsync(TModel model)
        {
            return await _repo.AddAsync(_mapper.Map<TEntity>(model));
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            var entity = await _repo.GetAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<IList<TModel>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<IList<TModel>> GetAllAsync(int? skip, int? take)
        {
            var entities = await _repo.GetAsync(new BaseFilterSpecification<TEntity>(skip, take, null, true));
            return _mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = await _repo.UpdateAsync(_mapper.Map<TEntity>(model));
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _repo.CountAsync();
        }
    }
}
