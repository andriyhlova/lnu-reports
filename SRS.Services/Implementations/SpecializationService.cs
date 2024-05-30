using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Services.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class SpecializationService : BaseService<Specialization>, ISpecializationService
    {
        public SpecializationService(IBaseRepository<Specialization> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<SpecializationModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var academicStatuses = await _repo.GetAsync(new SpecializationSpecification(filterModel));
            return _mapper.Map<IList<SpecializationModel>>(academicStatuses);
        }

        public async Task<int> CountAsync(BaseFilterModel filterModel)
        {
            var countFilterModel = new BaseFilterModel
            {
                Search = filterModel.Search
            };
            return await _repo.CountAsync(new SpecializationSpecification(countFilterModel));
        }
    }
}
