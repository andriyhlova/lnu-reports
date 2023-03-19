using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications;
using SRS.Domain.Specifications.UserSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class ReportService : BaseService<Report>, IReportService
    {
        private readonly IRoleActionService _roleActionService;
        private readonly IUserRepository _userRepository;

        public ReportService(IBaseRepository<Report> repo, IMapper mapper, IRoleActionService roleActionService, IUserRepository userRepository)
            : base(repo, mapper)
        {
            _roleActionService = roleActionService;
            _userRepository = userRepository;
        }

        public async Task<IList<BaseReportModel>> GetForUserAsync(UserAccountModel user, ReportFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<Report>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.User.Cathedra.FacultyId == user.FacultyId && (x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id))),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.User.CathedraId == user.CathedraId && (x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id))),
                [RoleNames.Worker] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.UserId == user.Id))
            };

            var reports = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<BaseReportModel>>(reports ?? new List<Report>());
        }

        public async Task<int> CountForUserAsync(UserAccountModel user, ReportFilterModel filterModel)
        {
            var countFilterModel = new ReportFilterModel
            {
                Search = filterModel.Search,
                CathedraId = filterModel.CathedraId,
                FacultyId = filterModel.FacultyId,
                UserId = filterModel.UserId,
                State = filterModel.State,
                From = filterModel.From,
                To = filterModel.To
            };

            var actions = new Dictionary<string, Func<Task<int>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.User.Cathedra.FacultyId == user.FacultyId && (x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id))),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.User.CathedraId == user.CathedraId && (x.State == ReportState.Confirmed || x.State == ReportState.Signed || x.UserId == user.Id))),
                [RoleNames.Worker] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.UserId == user.Id))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }

        public async Task<bool> ChangeState(int id, ReportState state)
        {
            var report = await _repo.GetAsync(id);
            if (report.Date.HasValue && !string.IsNullOrEmpty(report.Protocol))
            {
                report.State = state;
                await UpdateUserSnapshotAsync(report);
                await _repo.UpdateAsync(report);
                return true;
            }

            return false;
        }

        public async Task<ReportModel> GetUserReportAsync(string userId, int? reportId)
        {
            Report oldReport;
            if (!reportId.HasValue)
            {
                oldReport = await _repo.GetFirstOrDefaultAsync(x => x.State == ReportState.Draft && x.UserId == userId);
            }
            else
            {
                oldReport = await _repo.GetAsync(reportId.Value);
            }

            return _mapper.Map<ReportModel>(oldReport ?? new Report { UserId = userId });
        }

        public async Task<int> UpsertAsync<TModel>(TModel model, string currentUserId)
            where TModel : BaseModel
        {
            var report = new Report();
            if (model.Id != 0)
            {
                report = await _repo.GetAsync(model.Id, new BaseSpecification<Report>(asNoTracking: true));
                _mapper.Map(model, report);
                await _repo.UpdateAsync(report);
                return report.Id;
            }

            report.UserId = currentUserId;
            _mapper.Map(model, report);
            return await _repo.AddAsync(report);
        }

        public async Task<bool> DeleteAsync(int id, string currentUserId)
        {
            var report = await _repo.GetAsync(id, new BaseSpecification<Report>(asNoTracking: true));
            if (report.State == ReportState.Draft && report.UserId == currentUserId)
            {
                return await _repo.DeleteAsync(id);
            }

            return false;
        }

        private async Task UpdateUserSnapshotAsync(Report report)
        {
            var user = report.User;
            var cathedraLeads = await _userRepository.GetAsync(new CathedraLeadSpecification(user.CathedraId));
            report.UserFullName = user.I18nUserInitials.FirstOrDefault(x => x.Language == Language.EN)?.FullName;
            report.PositionName = user.Position.Value;
            report.CathedraName = user.Cathedra.GenitiveCase;
            report.CathedraLeadName = cathedraLeads.FirstOrDefault()?.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.ShortReverseFullName;
            report.GoogleScholarHIndex = user.GoogleScholarHIndex;
            report.ScopusHIndex = user.ScopusHIndex;
            report.WebOfScienceHIndex = user.WebOfScienceHIndex;
        }
    }
}
