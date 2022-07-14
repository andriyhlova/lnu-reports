using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;

namespace SRS.Services.Implementations
{
    public class ThemeOfScientificWorkService : BaseService<ThemeOfScientificWork>, IThemeOfScientificWorkService
    {
        public ThemeOfScientificWorkService(IBaseRepository<ThemeOfScientificWork> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<ThemeOfScientificWorkModel>> GetThemesForUserAsync(UserAccountModel user, int? skip, int? take)
        {
            var scientificThemes = await TakeRoleAction(
                user,
                async () =>
                {
                    var spec = new ThemeOfScientificWorkSpecification(skip, take, x => x.Cathedra.FacultyId == user.FacultyId);
                    return await _repo.GetAsync(spec);
                },
                async () =>
                {
                    var spec = new ThemeOfScientificWorkSpecification(skip, take, x => x.CathedraId == user.CathedraId);
                    return await _repo.GetAsync(spec);
                });

            return _mapper.Map<IList<ThemeOfScientificWorkModel>>(scientificThemes ?? new List<ThemeOfScientificWork>());
        }

        public async Task<int> CountThemesForUserAsync(UserAccountModel user)
        {
            return await TakeRoleAction(
                user,
                async () =>
                {
                    var spec = new ThemeOfScientificWorkSpecification(null, null, x => x.Cathedra.FacultyId == user.FacultyId);
                    return await _repo.CountAsync(spec);
                },
                async () =>
                {
                    var spec = new ThemeOfScientificWorkSpecification(null, null, x => x.CathedraId == user.CathedraId);
                    return await _repo.CountAsync(spec);
                });
        }

        private async Task<TItem> TakeRoleAction<TItem>(UserAccountModel user, Func<Task<TItem>> deaneryAction, Func<Task<TItem>> cathedraAction)
        {
            if (user.IsInRole(RoleNames.DeaneryAdmin))
            {
                return await deaneryAction();
            }
            else if (user.IsInRole(RoleNames.CathedraAdmin))
            {
                return await cathedraAction();
            }

            return default;
        }
    }
}
