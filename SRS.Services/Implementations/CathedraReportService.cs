using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.Constants;
using SRS.Services.Models.DepartmentReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IList<BaseDepartmentReportModel>> GetForUserAsync(UserAccountModel user, DepartmentReportFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<CathedraReport>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, x => x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, x => x.User.Cathedra.FacultyId == user.FacultyId && (x.State == ReportState.Signed || x.UserId == user.Id))),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new CathedraReportSpecification(filterModel, x => x.User.CathedraId == user.CathedraId))
            };

            var cathedraReports = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<BaseDepartmentReportModel>>(cathedraReports ?? new List<CathedraReport>());
        }

        public async Task<int> CountForUserAsync(UserAccountModel user, DepartmentReportFilterModel filterModel)
        {
            var countFilterModel = new DepartmentReportFilterModel
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
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, x => x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, x => x.User.Cathedra.FacultyId == user.FacultyId && (x.State == ReportState.Signed || x.UserId == user.Id))),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new CathedraReportSpecification(countFilterModel, x => x.User.CathedraId == user.CathedraId))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }

        public async Task<DepartmentReportModel> GetUserDepartmentReportAsync(string userId, int? reportId)
        {
            CathedraReport oldReport;
            if (!reportId.HasValue)
            {
                oldReport = await _repo.GetFirstOrDefaultAsync(x => x.State == ReportState.Draft && x.UserId == userId);
            }
            else
            {
                oldReport = await _repo.GetAsync(reportId.Value);
            }

            return _mapper.Map<DepartmentReportModel>(oldReport ?? new CathedraReport { UserId = userId });
        }

        public async Task<int> UpsertAsync<TModel>(TModel model, string currentUserId)
            where TModel : BaseModel
        {
            var report = new CathedraReport();
            if (model.Id != 0)
            {
                report = await _repo.GetAsync(model.Id, new BaseSpecification<CathedraReport>(asNoTracking: true));
                _mapper.Map(model, report);
                await _repo.UpdateAsync(report);
                return report.Id;
            }

            report.UserId = currentUserId;
            _mapper.Map(model, report);
            return await _repo.AddAsync(report);
        }

        public async Task<bool> ChangeState(int id, ReportState state)
        {
            var report = await _repo.GetAsync(id);
            if (report.Date.HasValue && !string.IsNullOrEmpty(report.Protocol))
            {
                report.State = state;
                await _repo.UpdateAsync(report);
                return true;
            }

            return false;
        }
    }
}
