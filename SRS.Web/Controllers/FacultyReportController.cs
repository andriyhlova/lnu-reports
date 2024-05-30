using AutoMapper;
using Microsoft.AspNet.Identity;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.DepartmentReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.DepartmentReports;
using SRS.Web.Models.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Адміністрація деканату")]
    public class FacultyReportController : Controller
    {
        private readonly IFacultyReportService _facultyReportService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IUserService<UserAccountModel> _userAccountService;
        private readonly IPublicationService _publicationService;
        private readonly IMapper _mapper;

        public FacultyReportController(
            IFacultyReportService facultyReportService,
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IUserService<UserAccountModel> userAccountService,
            IPublicationService publicationService,
            IMapper mapper)
        {
            _facultyReportService = facultyReportService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _userAccountService = userAccountService;
            _publicationService = publicationService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int? reportId, int? stepIndex)
        {
            var report = await _facultyReportService.GetUserDepartmentReportAsync(User.Identity.GetUserId(), reportId);
            var user = await _userAccountService.GetByIdAsync(report.UserId);
            var viewModel = _mapper.Map<DepartmentReportViewModel>(report);
            await FillPublications(viewModel, report);
            await FillThemes(user, report);
            await FillGrants(viewModel, report);
            FillStepIndex(stepIndex);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAchievementSchoolInfo(DepartmentReportAchievementSchoolModel reportOtherInfoViewModel, int? stepIndex)
        {
            var reportId = await _facultyReportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateThemeInfo(DepartmentReportGrantsViewModel reportGrantsModel, int? stepIndex)
        {
            var reportId = await _facultyReportService.UpsertAsync(_mapper.Map<DepartmentReportGrantsModel>(reportGrantsModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePublications(DepartmentReportPublicationsViewModel reportPublicationsModel, int? stepIndex)
        {
            var reportId = await _facultyReportService.UpsertAsync(_mapper.Map<DepartmentReportPublicationsModel>(reportPublicationsModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOtherInfo(DepartmentReportOtherInfoModel reportOtherInfoViewModel, int? stepIndex)
        {
            var reportId = await _facultyReportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateFinalInfo(DepartmentReportFinalInfoModel reportFinalInfoViewModel, int? stepIndex)
        {
            var reportId = await _facultyReportService.UpsertAsync(reportFinalInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalize(int id, int? stepIndex)
        {
            return RedirectToAction(nameof(Index), new { ReportId = id, StepIndex = stepIndex });
        }

        private async Task FillPublications(DepartmentReportViewModel viewModel, DepartmentReportModel cathedraReport)
        {
            var filterModel = new DepartmentReportPublicationFilterModel
            {
                DepartmentId = cathedraReport.DepartmentId,
                Date = cathedraReport.Date ?? DateTime.Now
            };

            var availablePublications = await _publicationService.GetAvailableDepartmentReportPublicationsAsync(filterModel, Departments.Faculty);
            viewModel.Publications = availablePublications.Where(x => x.PublicationType != PublicationType.Заявка_на_винахід && x.PublicationType != PublicationType.Патент)
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.PublicationsIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.ApplicationsForInvention = availablePublications.Where(x => x.PublicationType == PublicationType.Заявка_на_винахід)
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.ApplicationsForInventionIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.PatentsForInvention = availablePublications.Where(x => x.PublicationType == PublicationType.Патент)
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.PatentsForInventionIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        private async Task FillThemes(UserAccountModel user, DepartmentReportModel facultyReport)
        {
            ViewBag.AllThemes = await _themeOfScientificWorkService.GetActiveForDepartmentReportAsync(user.FacultyId.Value, facultyReport.Date, Departments.Faculty);
        }

        private async Task FillGrants(DepartmentReportViewModel viewModel, DepartmentReportModel facultyReport)
        {
            var availableGrants = await _themeOfScientificWorkService.GetGrantsForCathedraReportAsync(facultyReport.DepartmentId, facultyReport.Date);
            viewModel.Grants = availableGrants
                .Select(x => new CheckboxListItem()
                {
                    Checked = facultyReport.GrantsIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Value
                }).ToList();
        }

        private void FillStepIndex(int? stepIndex)
        {
            ViewBag.StepIndex = stepIndex ?? 0;
        }
    }
}