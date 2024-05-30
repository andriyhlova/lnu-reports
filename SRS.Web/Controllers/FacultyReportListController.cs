using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.DepartmentReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.DepartmentReports;
using SRS.Web.Models.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    public class FacultyReportListController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IFacultyReportService _facultyReportService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public FacultyReportListController(
            IFacultyService facultyService,
            IUserService<UserAccountModel> userService,
            IFacultyReportService facultyReportService,
            IExportService exportService,
            IMapper mapper)
        {
            _facultyService = facultyService;
            _userService = userService;
            _facultyReportService = facultyReportService;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(DepartmentReportFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<DepartmentReportFilterModel>(filterViewModel);
            var cathedraReports = await _facultyReportService.GetForUserAsync(user, filterModel);
            var total = await _facultyReportService.CountForUserAsync(user, filterModel);

            await FillAvailableDepartments();

            var viewModel = new ItemsViewModel<DepartmentReportFilterViewModel, BaseDepartmentReportModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BaseDepartmentReportModel>(cathedraReports, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(DepartmentReportFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<DepartmentReportFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var cathedraReports = await _facultyReportService.GetForUserAsync(user, filterModel);
            var csvModel = new CsvModel<CathedraReportCsvModel>
            {
                Data = _mapper.Map<IList<CathedraReportCsvModel>>(cathedraReports)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "facultyReport.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(DepartmentReportFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<DepartmentReportFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var cathedraReports = await _facultyReportService.GetForUserAsync(user, filterModel);
            var csvModel = new CsvModel<CathedraReportCsvModel>
            {
                Data = _mapper.Map<IList<CathedraReportCsvModel>>(cathedraReports)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "facultyReport.xlsx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sign(int reportId, DepartmentReportFilterViewModel filterViewModel)
        {
            await _facultyReportService.ChangeState(reportId, ReportState.Signed);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Negate(int reportId, DepartmentReportFilterViewModel filterViewModel)
        {
            await _facultyReportService.ChangeState(reportId, ReportState.Draft);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm(int reportId, DepartmentReportFilterViewModel filterViewModel)
        {
            await _facultyReportService.ChangeState(reportId, ReportState.Confirmed);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        private async Task FillAvailableDepartments()
        {
            ViewBag.AllFaculties = await _facultyService.GetAllAsync(new BaseFilterModel());
        }
    }
}
