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
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Implementations
{
    public class ThemeOfScientificWorkService : BaseService<ThemeOfScientificWork>, IThemeOfScientificWorkService
    {
        private readonly IRoleActionService _roleActionService;

        public ThemeOfScientificWorkService(IBaseRepository<ThemeOfScientificWork> repo, IMapper mapper, IRoleActionService roleActionService)
            : base(repo, mapper)
        {
            _roleActionService = roleActionService;
        }

        public async Task<int> AddAsync(UserAccountModel user, ThemeOfScientificWorkModel model)
        {
            var actions = new Dictionary<string, Func<Task<int?>>>
            {
                [RoleNames.CathedraAdmin] = () => Task.FromResult(user.CathedraId)
            };

            var entity = _mapper.Map<ThemeOfScientificWork>(model);
            entity.CathedraId = await _roleActionService.TakeRoleActionAsync(user, actions);
            return await _repo.AddAsync(entity);
        }

        public async Task<ThemeOfScientificWorkModel> UpdateAsync(UserAccountModel user, ThemeOfScientificWorkModel model)
        {
            var actions = new Dictionary<string, Func<Task<int?>>>
            {
                [RoleNames.CathedraAdmin] = () => Task.FromResult(user.CathedraId)
            };

            var entity = _mapper.Map<ThemeOfScientificWork>(model);
            entity.CathedraId = await _roleActionService.TakeRoleActionAsync(user, actions);
            entity = await _repo.UpdateAsync(_mapper.Map<ThemeOfScientificWork>(model));
            return _mapper.Map<ThemeOfScientificWorkModel>(entity);
        }

        public async Task<IList<ThemeOfScientificWorkModel>> GetThemesForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<ThemeOfScientificWork>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, x => x.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, x => x.CathedraId == user.CathedraId))
            };

            var scientificThemes = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<ThemeOfScientificWorkModel>>(scientificThemes ?? new List<ThemeOfScientificWork>());
        }

        public async Task<int> CountThemesForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel)
        {
            var countFilterModel = new DepartmentFilterModel
            {
                Search = filterModel.Search,
                CathedraId = filterModel.CathedraId,
                FacultyId = filterModel.FacultyId
            };

            var actions = new Dictionary<string, Func<Task<int>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.CountAsync(new ThemeOfScientificWorkSpecification(countFilterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new ThemeOfScientificWorkSpecification(countFilterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new ThemeOfScientificWorkSpecification(countFilterModel, x => x.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new ThemeOfScientificWorkSpecification(countFilterModel, x => x.CathedraId == user.CathedraId))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }
    }
}
