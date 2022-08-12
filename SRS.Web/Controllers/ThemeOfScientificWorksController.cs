using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Extensions;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Керівник кафедри, Адміністрація деканату")]
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
            var scientifthemes = await _themeOfScientificWorkService.GetForUserAsync(user, filterModel);
            var total = await _themeOfScientificWorkService.CountForUserAsync(user, filterModel);

            FillAllRelatedEntities();

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
        public ActionResult Create()
        {
            FillAllRelatedEntities();
            return View(new ThemeOfScientificWorkModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkCrudService.AddAsync(themeOfScientificWork);
                return RedirectToAction(nameof(Index));
            }

            FillAllRelatedEntities();
            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            FillAllRelatedEntities();
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkCrudService.UpdateAsync(themeOfScientificWork);
                return RedirectToAction(nameof(Index));
            }

            FillAllRelatedEntities();
            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _themeOfScientificWorkCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private void FillAllRelatedEntities()
        {
            FillFinancials();
        }

        private void FillFinancials()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Text = x.GetFriendlyName(), Value = x })
                .ToList();
        }
    }
}
