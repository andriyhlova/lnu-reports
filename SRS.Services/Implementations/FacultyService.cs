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
    public class FacultyService : BaseService<Faculty>, IFacultyService
    {
        public FacultyService(IBaseRepository<Faculty> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<FacultyModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var faculties = await _repo.GetAsync(new FacultySpecification(filterModel));
            return _mapper.Map<IList<FacultyModel>>(faculties);
        }

        public async Task<int> CountAsync(BaseFilterModel filterModel)
        {
            var countFilterModel = new BaseFilterModel
            {
                Search = filterModel.Search
            };
            return await _repo.CountAsync(new FacultySpecification(countFilterModel));
        }
    }
}
