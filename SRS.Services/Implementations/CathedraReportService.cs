using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Implementations
{
    public class CathedraReportService : BaseService<CathedraReport>, ICathedraReportService
    {
        private readonly IRoleActionService _roleActionService;

        public CathedraReportService(IBaseRepository<CathedraReport> repo, IMapper mapper, IRoleActionService roleActionService)
            : base(repo, mapper)
        {
            _roleActionService = roleActionService;
        }

        public async Task<IList<BaseCathedraReportModel>> GetForUserAsync(UserAccountModel user, CathedraReportFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<CathedraReport>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, x => x.User.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, x => x.User.CathedraId == user.CathedraId))
            };

            var cathedraReports = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<BaseCathedraReportModel>>(cathedraReports ?? new List<CathedraReport>());
        }

        public async Task<int> CountForUserAsync(UserAccountModel user, CathedraReportFilterModel filterModel)
        {
            var countFilterModel = new CathedraReportFilterModel
            {
                Search = filterModel.Search,
                CathedraId = filterModel.CathedraId,
                FacultyId = filterModel.FacultyId,
                From = filterModel.From,
                To = filterModel.To
            };

            var actions = new Dictionary<string, Func<Task<int>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, x => x.User.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, x => x.User.CathedraId == user.CathedraId))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }
    }
}
