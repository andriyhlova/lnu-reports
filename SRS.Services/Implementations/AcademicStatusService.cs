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
    public class AcademicStatusService : BaseService<AcademicStatus>, IAcademicStatusService
    {
        public AcademicStatusService(IBaseRepository<AcademicStatus> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<AcademicStatusModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var academicStatuses = await _repo.GetAsync(new AcademicStatusSpecification(filterModel));
            return _mapper.Map<IList<AcademicStatusModel>>(academicStatuses);
        }

        public async Task<int> CountAsync(BaseFilterModel filterModel)
        {
            var countFilterModel = new BaseFilterModel
            {
                Search = filterModel.Search
            };
            return await _repo.CountAsync(new AcademicStatusSpecification(countFilterModel));
        }
    }
}
