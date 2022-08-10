using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Implementations
{
    public class DegreeService : BaseService<Degree>, IDegreeService
    {
        public DegreeService(IBaseRepository<Degree> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<DegreeModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var degrees = await _repo.GetAsync(new DegreeSpecification(filterModel));
            return _mapper.Map<IList<DegreeModel>>(degrees);
        }

        public async Task<int> CountAsync(BaseFilterModel filterModel)
        {
            var countFilterModel = new BaseFilterModel
            {
                Search = filterModel.Search
            };
            return await _repo.CountAsync(new DegreeSpecification(countFilterModel));
        }
    }
}
