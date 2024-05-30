using AutoMapper;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Services.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class DissertationDefenseService : BaseService<DissertationDefense>, IDissertationDefenseService
    {
        public DissertationDefenseService(IBaseRepository<DissertationDefense> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<int> CountAsync(DissertationDefenseFilterModel filterModel)
        {
            var countFilterModel = new DissertationDefenseFilterModel
            {
                Search = filterModel.Search,
                PeriodFrom = filterModel.PeriodFrom,
                PeriodTo = filterModel.PeriodTo,
                FacultyId = filterModel.FacultyId,
                CathedraId = filterModel.CathedraId
            };

            return await _repo.CountAsync(new DissertationDefenseSpecification(countFilterModel, null));
        }

        public async Task<IList<DissertationDefenseModel>> GetAsync(DissertationDefenseFilterModel filterModel)
        {
            var dissertationDefense = await _repo.GetAsync(new DissertationDefenseSpecification(filterModel, null));
            return _mapper.Map<IList<DissertationDefenseModel>>(dissertationDefense);
        }
    }
}
