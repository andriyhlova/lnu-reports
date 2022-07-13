using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Services.Implementations
{
    public class CathedraService : BaseCrudService<Cathedra, CathedraModel>, ICathedraService
    {
        public CathedraService(IBaseRepository<Cathedra> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<CathedraModel>> GetByFacultyAsync(int? facultyId)
        {
            if (facultyId == null)
            {
                return await GetAllAsync();
            }

            var cathedras = await _repo.GetAsync(x => x.FacultyId == facultyId);
            return _mapper.Map<IList<CathedraModel>>(cathedras);
        }
    }
}
