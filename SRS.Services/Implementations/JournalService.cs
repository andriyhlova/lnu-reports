using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.JournalModels;

namespace SRS.Services.Implementations
{
    public class JournalService : BaseService<Journal>, IJournalService
    {
        public JournalService(IBaseRepository<Journal> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<JournalModel>> GetAllAsync(JournalFilterModel filterModel)
        {
            var journals = await _repo.GetAsync(new JournalSpecification(filterModel));
            return _mapper.Map<IList<JournalModel>>(journals);
        }

        public async Task<int> CountAsync(JournalFilterModel filterModel)
        {
            var countFilterModel = new JournalFilterModel
            {
                Search = filterModel.Search
            };
            return await _repo.CountAsync(new JournalSpecification(countFilterModel));
        }
    }
}
