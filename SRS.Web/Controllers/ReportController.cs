using AutoMapper;
using Microsoft.AspNet.Identity;
using Rotativa;
using SRS.Repositories.Context;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManagement.Services;

namespace SRS.Web.Controllers
{
    public class ReportController : Controller
    {
        private ReportService reportService;

        private readonly IReportService _reportService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IUserService<UserAccountModel> _userAccountService;
        private readonly IPublicationService _publicationService;
        private readonly IMapper _mapper;

        public ReportController(
            IReportService __reportService, 
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IUserService<UserAccountModel> userAccountService,
            IPublicationService publicationService,
            IMapper mapper)
        {
            reportService = new ReportService(new ApplicationDbContext());
            _reportService = __reportService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _userAccountService = userAccountService;
            _publicationService = publicationService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int? reportId, int? stepIndex, ReportPublicationsFilterViewModel publicationDateFilter)
        {
            var report = await _reportService.GetUserReportAsync(User.Identity.GetUserId(), reportId);
            var user = await _userAccountService.GetByIdAsync(report.UserId);
            var viewModel = _mapper.Map<ReportViewModel>(report);
            await FillPublications(viewModel, report, publicationDateFilter);
            await FillThemeOfScientificWorks(user.FacultyId);
            FillFilters(publicationDateFilter);
            FillStepIndex(stepIndex);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePublications(ReportPublicationsViewModel reportPublicationsViewModel, int? stepIndex)
        {
            await _reportService.UpsertAsync(_mapper.Map<ReportPublicationsModel>(reportPublicationsViewModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportPublicationsViewModel.Id, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateScientificWork(ReportScientificWorkModel reportScientificWorkModel, int? stepIndex)
        {
            await _reportService.UpsertAsync(reportScientificWorkModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportScientificWorkModel.Id, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateOtherInfo(ReportOtherInfoModel reportOtherInfoViewModel, int? stepIndex)
        {
            await _reportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportOtherInfoViewModel.Id, StepIndex = stepIndex });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateFinalInfo(ReportFinalInfoModel reportFinalInfoViewModel, int? stepIndex)
        {
            await _reportService.UpsertAsync(reportFinalInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportFinalInfoViewModel.Id, StepIndex = stepIndex });
        }

        [HttpPost]
        public ActionResult Finalize(int id, int? stepIndex)
        {
            return RedirectToAction(nameof(Index), new { ReportId = id, StepIndex = stepIndex });
        }

        [HttpGet]
        public ActionResult Preview(int reportId)
        {
            return Content(reportService.GenerateHTMLReport(reportId));
        }

        [HttpGet]
        public ActionResult PreviewPdf(int reportId)
        {
            return new ActionAsPdf(nameof(Preview), new { reportId = reportId});
        }

        public ActionResult GetLatex(int reportId)
        {
            string content = reportService.GenerateHTMLReport(reportId);
            var file = Path.Combine(ConfigurationManager.AppSettings["HtmlFilePath"],@"test.html");
            System.IO.File.WriteAllText(file, content);
            string result = "";
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(ConfigurationManager.AppSettings["PandocPath"], "pandoc.exe"),
                    Arguments = $@"--from html {file} --to latex -s --wrap=preserve",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.GetEncoding(866)
                }
            };
            proc.Start();
            int i = 0;
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                result += line;
                result += "\n";
                i++;
                if (i == 8)
                {
                    result += @"\usepackage[ukrainian]{babel}" + "\n";
                }
            }

            return File(System.Text.Encoding.GetEncoding(866).GetBytes(result), "application/x-latex", "report.tex");
        }

        private async Task FillThemeOfScientificWorks(int? facultyId)
        {
            var themes = await _themeOfScientificWorkService.GetActiveForFacultyAsync(facultyId);
            ViewBag.ScientificThemesByFaculty = _mapper.Map<IList<SelectListItem>>(themes); 
        }

        private async Task FillPublications(ReportViewModel viewModel, ReportModel report, ReportPublicationsFilterViewModel publicationDateFilter)
        {
            var filterModel = new ReportPublicationFilterModel
            {
                From = publicationDateFilter.PublicationDateFrom,
                To = publicationDateFilter.PublicationDateTo,
                UserId = report.UserId
            };
            var availablePublications = await _publicationService.GetAvailableReportPublicationsAsync(filterModel);
            viewModel.RecomendedPublication = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = report.RecomendedPublicationIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.PrintedPublication = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = report.PrintedPublicationIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.AcceptedToPrintPublication = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = report.AcceptedToPrintPublicationIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        private void FillFilters(ReportPublicationsFilterViewModel publicationDateFilter)
        {
            ViewBag.PublicationDateFrom = publicationDateFilter.PublicationDateFrom;
            ViewBag.PublicationDateTo = publicationDateFilter.PublicationDateTo;
        }

        private void FillStepIndex(int? stepIndex)
        {
            ViewBag.StepIndex = stepIndex ?? 0;
        }
    }
}