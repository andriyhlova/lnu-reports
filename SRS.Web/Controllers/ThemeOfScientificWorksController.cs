using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManagement.Extensions;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Керівник кафедри, Адміністрація деканату")]
    public class ThemeOfScientificWorksController : Controller
    {
        private readonly IBaseCrudService<CathedraModel> _cathedraCrudService;
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<ThemeOfScientificWorkModel> _themeOfScientificWorkCrudService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IMapper _mapper;

        public ThemeOfScientificWorksController(
            IBaseCrudService<CathedraModel> cathedraCrudService,
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<ThemeOfScientificWorkModel> themeOfScientificWorkCrudService,
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IUserService<UserAccountModel> userService,
            IMapper mapper)
        {
            _cathedraCrudService = cathedraCrudService;
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _themeOfScientificWorkCrudService = themeOfScientificWorkCrudService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(DepartmentFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<DepartmentFilterModel>(filterViewModel);
            var scientifthemes = await _themeOfScientificWorkService.GetThemesForUserAsync(user, filterModel);
            var total = await _themeOfScientificWorkService.CountThemesForUserAsync(user, filterModel);

            await FillAvailableDepartments(user.FacultyId);

            var viewModel = new ItemsViewModel<DepartmentFilterViewModel, ThemeOfScientificWorkModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<ThemeOfScientificWorkModel>(scientifthemes, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var themeOfScientificWork = await _themeOfScientificWorkCrudService.GetAsync(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            await FillAllRelatedEntities(user.FacultyId);
            FillFinancials();
            return View(new ThemeOfScientificWorkModel { CathedraId = user.CathedraId, FacultyId = user.FacultyId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkService.AddAsync(user, themeOfScientificWork);
                return RedirectToAction(nameof(Index));
            }

            await FillAllRelatedEntities(user.FacultyId);
            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            await FillAllRelatedEntities(user.FacultyId);
            var themeOfScientificWork = await _themeOfScientificWorkCrudService.GetAsync(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkService.UpdateAsync(user, themeOfScientificWork);
                return RedirectToAction(nameof(Index));
            }

            await FillAllRelatedEntities(user.FacultyId);
            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var themeOfScientificWork = await _themeOfScientificWorkCrudService.GetAsync(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _themeOfScientificWorkCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task FillAllRelatedEntities(int? facultyId)
        {
            await FillAvailableDepartments(facultyId);
            FillFinancials();
        }

        private void FillFinancials()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Text = x.GetFriendlyName(), Value = x })
                .ToList();
        }

        private async Task FillAvailableDepartments(int? facultyId)
        {
            if (User.IsInRole(RoleNames.Superadmin) || User.IsInRole(RoleNames.RectorateAdmin))
            {
                ViewBag.AllCathedras = await _cathedraCrudService.GetAllAsync();
                ViewBag.AllFaculties = await _facultyService.GetAllAsync();
            }
            else if (User.IsInRole(RoleNames.DeaneryAdmin))
            {
                ViewBag.AllCathedras = await _cathedraService.GetByFacultyAsync(facultyId);
            }
        }
    }
}
