using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;

namespace SRS.Services.Implementations
{
    public class ThemeOfScientificWorkService : BaseCrudService<ThemeOfScientificWork, ThemeOfScientificWorkModel>, IThemeOfScientificWorkService
    {
        public ThemeOfScientificWorkService(IBaseRepository<ThemeOfScientificWork> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<ThemeOfScientificWorkModel>> GetThemesForUserAsync(UserAccountModel user)
        {
            var scientificThemes = new List<ThemeOfScientificWork>();
            if (user.IsInRole(RoleNames.DeaneryAdmin))
            {
                // ANDR include and order by period to descending
                scientificThemes = await _repo.GetAsync(x => x.Cathedra.Faculty.Id == user.FacultyId);
            }
            else if (user.IsInRole(RoleNames.CathedraAdmin))
            {
                // ANDR include and order by period to descending
                scientificThemes = await _repo.GetAsync(x => x.CathedraId == user.CathedraId);
            }

            return _mapper.Map<IList<ThemeOfScientificWorkModel>>(scientificThemes);
        }
    }
}
