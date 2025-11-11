using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.JournalModels;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (filterModel.PublicationType == PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q1_Q2)
            {
                filterModel.Quartiles.AddRange(new List<Quartile?> { Quartile.Q1, Quartile.Q2 });
            }

            if (filterModel.PublicationType == PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q3_Q4)
            {
                filterModel.Quartiles.AddRange(new List<Quartile?> { Quartile.Q3, Quartile.Q4, Quartile.None });
            }

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
