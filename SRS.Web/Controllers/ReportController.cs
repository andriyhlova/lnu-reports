using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Працівник")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IPublicationService _publicationService;
        private readonly IMapper _mapper;

        public ReportController(
            IReportService reportService,
            IPublicationService publicationService,
            IMapper mapper)
        {
            _reportService = reportService;
            _publicationService = publicationService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(int? reportId, int? stepIndex, ReportPublicationsFilterViewModel publicationDateFilter)
        {
            var report = await _reportService.GetUserReportAsync(User.Identity.GetUserId(), reportId);
            var viewModel = _mapper.Map<ReportViewModel>(report);
            await FillPublications(viewModel, report, publicationDateFilter);
            FillFilters(publicationDateFilter);
            FillStepIndex(stepIndex);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePublications(ReportPublicationsViewModel reportPublicationsViewModel, int? stepIndex)
        {
            var reportId = await _reportService.UpsertAsync(_mapper.Map<ReportPublicationsModel>(reportPublicationsViewModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateScientificWork(ReportScientificWorkViewModel reportScientificWorkViewModel, int? stepIndex)
        {
            var reportId = await _reportService.UpsertAsync(_mapper.Map<ReportScientificWorkModel>(reportScientificWorkViewModel), User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateOtherInfo(ReportOtherInfoModel reportOtherInfoViewModel, int? stepIndex)
        {
            var reportId = await _reportService.UpsertAsync(reportOtherInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateFinalInfo(ReportFinalInfoModel reportFinalInfoViewModel, int? stepIndex)
        {
            var reportId = await _reportService.UpsertAsync(reportFinalInfoViewModel, User.Identity.GetUserId());
            return RedirectToAction(nameof(Index), new { ReportId = reportId, StepIndex = stepIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalize(int id, int? stepIndex)
        {
            return RedirectToAction(nameof(Index), new { ReportId = id, StepIndex = stepIndex });
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            var report = await _reportService.GetUserReportAsync(User.Identity.GetUserId(), id);
            if (report == null)
            {
                return HttpNotFound();
            }

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View(report);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _reportService.DeleteAsync(id, User.Identity.GetUserId());
            var returnUrl = Request.QueryString["returnUrl"];
            return Redirect(Url.Action(nameof(Index), "ReportList") + (!string.IsNullOrWhiteSpace(returnUrl) ? "?" + returnUrl : string.Empty));
        }

        private async Task FillPublications(ReportViewModel viewModel, ReportModel report, ReportPublicationsFilterViewModel publicationDateFilter)
        {
            var filterModel = new ReportPublicationFilterModel
            {
                From = publicationDateFilter.PublicationDateFrom,
                To = publicationDateFilter.PublicationDateTo,
                UserId = report.UserId
            };
            var publications = await _publicationService.GetAvailableReportPublicationsAsync(filterModel);
            var availablePublications = publications.Where(x => x.PublicationType != PublicationType.Заявка_на_винахід && x.PublicationType != PublicationType.Патент);

            viewModel.StudentPublication = availablePublications
                .Select(x => new CheckboxListItem()
                {
                    Checked = report.StudentPublicationIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

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

            viewModel.ApplicationsForInvention = publications.Where(x => x.PublicationType == PublicationType.Заявка_на_винахід)
                .Select(x => new CheckboxListItem()
                {
                    Checked = report.ApplicationsForInventionIds.Any(y => y == x.Id),
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            viewModel.PatentsForInvention = publications.Where(x => x.PublicationType == PublicationType.Патент)
                .Select(x => new CheckboxListItem()
                {
                    Checked = report.PatentsForInventionIds.Any(y => y == x.Id),
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