using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Services.Implementations
{
    public class BaseCrudService<TEntity, TModel> : IBaseCrudService<TModel>
        where TModel : BaseModel
        where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repo;
        private readonly IMapper _mapper;

        public BaseCrudService(IBaseRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<int> Add(TModel model)
        {
            return await _repo.Add(_mapper.Map<TEntity>(model));
        }

        public virtual async Task<TModel> Get(int id)
        {
            var entity = await _repo.Get(id);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<IList<TModel>> GetAll()
        {
            var entities = await _repo.GetAll();
            return _mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<TModel> Update(TModel model)
        {
            var entity = await _repo.Update(_mapper.Map<TEntity>(model));
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }
    }
}
