using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Reports;
using SRS.Web.Models.Shared;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UserManagement.Controllers
{
    [Authorize]
    public class ReportListController : Controller
    {
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportListController(
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IUserService<UserAccountModel> userService,
            IReportService reportService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _userService = userService;
            _reportService = reportService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(ReportFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<ReportFilterModel>(filterViewModel);
            var reports = await _reportService.GetReportsForUserAsync(user, filterModel);
            var total = await _reportService.CountReportsForUserAsync(user, filterModel);

            await FillAvailableDepartments(user.FacultyId);

            var viewModel = new ItemsViewModel<ReportFilterViewModel, BaseReportModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BaseReportModel>(reports, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Sign(int reportId, ReportFilterViewModel filterViewModel)
        {
            await _reportService.SignAsync(reportId);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Negate(int reportId, ReportFilterViewModel filterViewModel)
        {
            await _reportService.ReturnAsync(reportId);
            return RedirectToAction(nameof(Index), filterViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(int reportId, ReportFilterViewModel filterViewModel)
        {
            await _reportService.ConfirmAsync(reportId);
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
