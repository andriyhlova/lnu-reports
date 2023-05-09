using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Services.Specifications;

namespace SRS.Services.Implementations
{
    public class CathedraService : BaseService<Cathedra>, ICathedraService
    {
        public CathedraService(IBaseRepository<Cathedra> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<CathedraModel>> GetByFacultyAsync(int? facultyId)
        {
            if (facultyId == null)
            {
                return _mapper.Map<IList<CathedraModel>>(await _repo.GetAllAsync());
            }

            var cathedras = await _repo.GetAsync(x => x.FacultyId == facultyId);
            return _mapper.Map<IList<CathedraModel>>(cathedras);
        }

        public async Task<IList<CathedraModel>> GetAllAsync(BaseFilterModel filterModel)
        {
            var cathedras = await _repo.GetAsync(new CathedraSpecification(filterModel));
            return _mapper.Map<IList<CathedraModel>>(cathedras);
        }
    }
}
