using AutoMapper;
using Microsoft.AspNet.Identity;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.CathedraReports;
using SRS.Web.Models.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class CathedraReportController : Controller
    {
        private readonly ICathedraReportService _cathedraReportService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IUserService<UserAccountModel> _userAccountService;
        private readonly IPublicationService _publicationService;
        private readonly IMapper _mapper;

        public CathedraReportController(
            ICathedraReportService cathedraReportService,
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IUserService<UserAccountModel> userAccountService,
            IPublicationService publicationService,
            IMapper mapper)
        {
            _cathedraReportService = cathedraReportService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _userAccountService = userAccountService;
            _publicationService = publicationService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int? reportId, int? stepIndex)
        {
            var report = await _cathedraReportService.GetUserCathedraReportAsync(User.Identity.GetUserId(), reportId);
            var user = await _userAccountService.GetByIdAsync(report.UserId);
            var viewModel = _mapper.Map<CathedraReportViewModel>(report);
            await FillPublications(viewModel, report);
            await FillThemes(user, report);
            await FillGrants(viewModel, report);
            FillStepIndex(stepIndex);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAchievementSchoolInfo(CathedraReportAchievementSchoolModel reportOtherInfoViewModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateThemeInfo(CathedraReportGrantsViewModel reportGrantsModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(_mapper.Map<CathedraReportGrantsModel>(reportGrantsModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePublications(CathedraReportPublicationsViewModel reportPublicationsModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(_mapper.Map<CathedraReportPublicationsModel>(reportPublicationsModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOtherInfo(CathedraReportOtherInfoModel reportOtherInfoViewModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateFinalInfo(CathedraReportFinalInfoModel reportFinalInfoViewModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(reportFinalInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalize(int id, int? stepIndex)
        {
            return RedirectToAction(nameof(Index), new { ReportId = id, StepIndex = stepIndex });
        }

        private async Task FillPublications(CathedraReportViewModel viewModel, CathedraReportModel cathedraReport)
        {
            var filterModel = new CathedraReportPublicationFilterModel
            {
                CathedraId = cathedraReport.CathedraId,
                Date = cathedraReport.Date ?? DateTime.Now
            };

            var availablePublications = await _publicationService.GetAvailableCathedraReportPublicationsAsync(filterModel);
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

        private async Task FillThemes(UserAccountModel user, CathedraReportModel cathedraReport)
        {
            ViewBag.AllThemes = await _themeOfScientificWorkService.GetActiveForCathedraReportAsync(user.CathedraId.Value, cathedraReport.Date ?? DateTime.Now);
        }

        private async Task FillGrants(CathedraReportViewModel viewModel, CathedraReportModel cathedraReport)
        {
            var availableGrants = await _themeOfScientificWorkService.GetGrantsForCathedraReportAsync(cathedraReport.CathedraId, cathedraReport.Date ?? DateTime.Now);
            viewModel.Grants = availableGrants
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.GrantsIds.Any(y => y == x.Id),
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