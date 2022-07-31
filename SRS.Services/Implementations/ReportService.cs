using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Implementations
{
    public class ReportService : BaseService<Report>, IReportService
    {
        private readonly IRoleActionService _roleActionService;

        public ReportService(IBaseRepository<Report> repo, IMapper mapper, IRoleActionService roleActionService)
            : base(repo, mapper)
        {
            _roleActionService = roleActionService;
        }

        public async Task<IList<BaseReportModel>> GetForUserAsync(UserAccountModel user, ReportFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<Report>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.IsConfirmed || x.IsSigned || x.UserId == user.Id)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.User.Cathedra.FacultyId == user.FacultyId && (x.IsConfirmed || x.IsSigned || x.UserId == user.Id))),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new ReportSpecification(filterModel, x => x.User.CathedraId == user.CathedraId && (x.IsSigned || x.UserId == user.Id))),
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
                From = filterModel.From,
                To = filterModel.To
            };

            var actions = new Dictionary<string, Func<Task<int>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.IsConfirmed || x.IsSigned || x.UserId == user.Id)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.User.Cathedra.FacultyId == user.FacultyId && (x.IsConfirmed || x.IsSigned || x.UserId == user.Id))),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.User.CathedraId == user.CathedraId && (x.IsSigned || x.UserId == user.Id))),
                [RoleNames.Worker] = async () => await _repo.CountAsync(new ReportSpecification(countFilterModel, x => x.UserId == user.Id))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }

        public async Task<bool> SignAsync(int id)
        {
            var report = await _repo.GetAsync(id);
            report.IsSigned = true;
            await _repo.UpdateAsync(report);
            return true;
        }

        public async Task<bool> ConfirmAsync(int id)
        {
            var report = await _repo.GetAsync(id);
            report.IsSigned = true;
            report.IsConfirmed = true;
            await _repo.UpdateAsync(report);
            return true;
        }

        public async Task<bool> ReturnAsync(int id)
        {
            var report = await _repo.GetAsync(id);
            report.IsSigned = false;
            report.IsConfirmed = false;
            await _repo.UpdateAsync(report);
            return true;
        }

        public async Task<ReportModel> GetUserReportAsync(string userId, int? reportId)
        {
            Report oldReport;
            if (!reportId.HasValue)
            {
                oldReport = await _repo.GetFirstOrDefaultAsync(x => !x.IsSigned && x.UserId == userId);
            }
            else
            {
                oldReport = await _repo.GetAsync(reportId.Value);
            }

            return _mapper.Map<ReportModel>(oldReport ?? new Report());
        }

        public async Task<bool> UpsertAsync<TModel>(TModel model, string currentUserId)
            where TModel : BaseModel
        {
            var report = new Report();
            if (model.Id != 0)
            {
                report = await _repo.GetAsync(model.Id, new BaseSpecification<Report>(asNoTracking: true));
                _mapper.Map(model, report);
                await _repo.UpdateAsync(report);
                return true;
            }

            report.UserId = currentUserId;
            _mapper.Map(model, report);
            await _repo.AddAsync(report);
            return true;
        }
    }
}
