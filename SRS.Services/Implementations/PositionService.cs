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
    public class PositionService : BaseService<Position>, IPositionService
    {
        public PositionService(IBaseRepository<Position> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<PositionModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var academicStatuses = await _repo.GetAsync(new PositionSpecification(filterModel));
            return _mapper.Map<IList<PositionModel>>(academicStatuses);
        }
    }
}
