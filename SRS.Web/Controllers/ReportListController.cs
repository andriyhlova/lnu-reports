using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize]
    public class ReportListController : Controller
    {
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IReportService _reportService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public ReportListController(
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IUserService<UserAccountModel> userService,
            IReportService reportService,
            IExportService exportService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _userService = userService;
            _reportService = reportService;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(ReportFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<ReportFilterModel>(filterViewModel);
            var reports = await _reportService.GetForUserAsync(user, filterModel);
            var total = await _reportService.CountForUserAsync(user, filterModel);

            await FillAvailableDepartments(user.FacultyId);

            var viewModel = new ItemsViewModel<ReportFilterViewModel, BaseReportModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BaseReportModel>(reports, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(ReportFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<ReportFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var reports = await _reportService.GetForUserAsync(user, filterModel);
            var csvModel = new CsvModel<ReportCsvModel>
            {
                Data = _mapper.Map<IList<ReportCsvModel>>(reports)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "report.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(ReportFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<ReportFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var reports = await _reportService.GetForUserAsync(user, filterModel);
            var csvModel = new CsvModel<ReportCsvModel>
            {
                Data = _mapper.Map<IList<ReportCsvModel>>(reports)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "report.xlsx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sign(int reportId, ReportFilterViewModel filterViewModel)
        {
            await _reportService.ChangeState(reportId, ReportState.Signed);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Negate(int reportId, ReportFilterViewModel filterViewModel)
        {
            await _reportService.ChangeState(reportId, ReportState.Draft);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm(int reportId, ReportFilterViewModel filterViewModel)
        {
            await _reportService.ChangeState(reportId, ReportState.Confirmed);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        private async Task FillAvailableDepartments(int? facultyId)
        {
            if (User.IsInRole(RoleNames.Superadmin) || User.IsInRole(RoleNames.RectorateAdmin))
            {
                ViewBag.AllCathedras = await _cathedraService.GetByFacultyAsync(null);
                ViewBag.AllFaculties = await _facultyService.GetAllAsync();
            }
            else if (User.IsInRole(RoleNames.DeaneryAdmin))
            {
                ViewBag.AllCathedras = await _cathedraService.GetByFacultyAsync(facultyId);
            }
        }
    }
}
