using AutoMapper;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;

namespace SRS.Services.Implementations
{
    public class BaseService<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> _repo;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
