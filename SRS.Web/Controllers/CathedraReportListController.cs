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
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.CathedraReports;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    public class CathedraReportListController : Controller
    {
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly ICathedraReportService _cathedraReportService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public CathedraReportListController(
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IUserService<UserAccountModel> userService,
            ICathedraReportService cathedraReportService,
            IExportService exportService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _userService = userService;
            _cathedraReportService = cathedraReportService;
            _exportService = exportService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(CathedraReportFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<CathedraReportFilterModel>(filterViewModel);
            var cathedraReports = await _cathedraReportService.GetForUserAsync(user, filterModel);
            var total = await _cathedraReportService.CountForUserAsync(user, filterModel);

            await FillAvailableDepartments(user.FacultyId);

            var viewModel = new ItemsViewModel<CathedraReportFilterViewModel, BaseCathedraReportModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BaseCathedraReportModel>(cathedraReports, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(CathedraReportFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<CathedraReportFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var cathedraReports = await _cathedraReportService.GetForUserAsync(user, filterModel);
            var csvModel = new CsvModel<CathedraReportCsvModel>
            {
                Data = _mapper.Map<IList<CathedraReportCsvModel>>(cathedraReports)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "cathedraReport.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(CathedraReportFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<CathedraReportFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var cathedraReports = await _cathedraReportService.GetForUserAsync(user, filterModel);
            var csvModel = new CsvModel<CathedraReportCsvModel>
            {
                Data = _mapper.Map<IList<CathedraReportCsvModel>>(cathedraReports)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "cathedraReport.xlsx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sign(int reportId, CathedraReportFilterViewModel filterViewModel)
        {
            await _cathedraReportService.ChangeState(reportId, ReportState.Signed);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Negate(int reportId, CathedraReportFilterViewModel filterViewModel)
        {
            await _cathedraReportService.ChangeState(reportId, ReportState.Draft);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm(int reportId, CathedraReportFilterViewModel filterViewModel)
        {
            await _cathedraReportService.ChangeState(reportId, ReportState.Confirmed);
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
