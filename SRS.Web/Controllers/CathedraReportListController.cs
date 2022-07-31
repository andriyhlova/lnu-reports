using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.CathedraReports;
using SRS.Web.Models.Shared;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Керівник кафедри, Адміністрація деканату")]
    public class CathedraReportListController : Controller
    {
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly ICathedraReportService _cathedraReportService;
        private readonly IMapper _mapper;

        public CathedraReportListController(ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IUserService<UserAccountModel> userService,
            ICathedraReportService cathedraReportService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _userService = userService;
            _cathedraReportService = cathedraReportService;
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
