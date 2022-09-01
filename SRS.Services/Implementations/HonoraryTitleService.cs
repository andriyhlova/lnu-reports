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
    public class HonoraryTitleService : BaseService<HonoraryTitle>, IHonoraryTitleService
    {
        public HonoraryTitleService(IBaseRepository<HonoraryTitle> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<HonoraryTitleModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var honraryTitles = await _repo.GetAsync(new HonoraryTitleSpecification(filterModel));
            return _mapper.Map<IList<HonoraryTitleModel>>(honraryTitles);
        }
    }
}
