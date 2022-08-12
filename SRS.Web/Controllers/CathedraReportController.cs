using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Enums;
using SRS.Web.Models.CathedraReports;
using SRS.Web.Models.Shared;

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
            var financial = GetFinancialByStep(stepIndex);
            await FillPublications(viewModel, report, financial);
            await FillThemeOfScientificWorks(user.CathedraId.Value, financial);
            FillFilters(financial);
            FillStepIndex(stepIndex);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBudgetThemeInfo(CathedraReportBudgetThemeViewModel reportBudgetThemeViewModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(_mapper.Map<CathedraReportBudgetThemeModel>(reportBudgetThemeViewModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateInWorkThemeInfo(CathedraReportInTimeThemeViewModel reportInTimeThemeModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(_mapper.Map<CathedraReportInTimeThemeModel>(reportInTimeThemeModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateHospDohovirThemeInfo(CathedraReportHospDohovirThemeViewModel reportHospDohovirThemeModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(_mapper.Map<CathedraReportHospDohovirThemeModel>(reportHospDohovirThemeModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateOtherInfo(CathedraReportOtherInfoModel reportOtherInfoViewModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateFinalInfo(CathedraReportFinalInfoModel reportFinalInfoViewModel, int? stepIndex)
        {
            var reportId = await _cathedraReportService.UpsertAsync(reportFinalInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        public ActionResult Finalize(int id, int? stepIndex)
        {
            return RedirectToAction(nameof(Index), new { ReportId = id, StepIndex = stepIndex });
        }

        private Financial? GetFinancialByStep(int? stepIndex)
        {
            var cathedraReportStep = (CathedraReportStep?)stepIndex;
            switch (cathedraReportStep)
            {
                case CathedraReportStep.BudgetThemeInfo: return Financial.Budget;
                case CathedraReportStep.InTimeTHemeInfo: return Financial.InWorkTime;
                case CathedraReportStep.HospDohovirThemeInfo: return Financial.BusinessContract;
                default: return null;
            }
        }

        private async Task FillThemeOfScientificWorks(int cathedraId, Financial? financial)
        {
            if (!financial.HasValue)
            {
                ViewBag.ScientificThemesByFaculty = new List<SelectListItem>();
                ViewBag.AllThemeDescriptions = new List<SelectListItem>();
                return;
            }

            var themes = await _themeOfScientificWorkService.GetActiveForCathedraReportAsync(cathedraId, financial.Value);
            ViewBag.ScientificThemesByFaculty = _mapper.Map<IList<SelectListItem>>(themes);
            ViewBag.AllThemeDescriptions = _mapper.Map<IList<SelectListItem>>(themes);
        }

        private async Task FillPublications(CathedraReportViewModel viewModel, CathedraReportModel cathedraReport, Financial? financial)
        {
            if (!financial.HasValue)
            {
                viewModel.PrintedPublicationBudgetTheme = new List<CheckboxListItem>();
                viewModel.PrintedPublicationHospDohovirTheme = new List<CheckboxListItem>();
                viewModel.PrintedPublicationThemeInWorkTime = new List<CheckboxListItem>();
                return;
            }

            var filterModel = new CathedraReportPublicationFilterModel
            {
                CathedraId = cathedraReport.CathedraId,
                Financial = financial.Value
            };
            var availablePublications = await _publicationService.GetAvailableCathedraReportPublicationsAsync(filterModel);
            viewModel.PrintedPublicationBudgetTheme = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.PrintedPublicationBudgetThemeIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.PrintedPublicationHospDohovirTheme = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.PrintedPublicationHospDohovirThemeIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.PrintedPublicationThemeInWorkTime = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = cathedraReport.PrintedPublicationThemeInWorkTimeIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        private void FillFilters(Financial? financial)
        {
            ViewBag.Financial = financial;
        }

        private void FillStepIndex(int? stepIndex)
        {
            ViewBag.StepIndex = stepIndex ?? 0;
        }
    }
}