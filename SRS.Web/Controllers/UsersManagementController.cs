using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.Shared;
using SRS.Web.Models.UsersManagement;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    public class UsersManagementController : Controller
    {
        private readonly IBaseCrudService<CathedraModel> _cathedraCrudService;
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IUserService<BaseUserInfoModel> _baseUserInfoService;
        private readonly IUserService<UserInfoModel> _userInfoService;
        private readonly IUserService<UserAccountModel> _userAccountService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UsersManagementController(
            IBaseCrudService<CathedraModel> cathedraCrudService,
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IUserService<BaseUserInfoModel> baseUserInfoService,
            IUserService<UserInfoModel> userInfoService,
            IUserService<UserAccountModel> userAccountService,
            IRoleService roleService,
            IMapper mapper)
        {
            _cathedraCrudService = cathedraCrudService;
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _baseUserInfoService = baseUserInfoService;
            _userInfoService = userInfoService;
            _userAccountService = userAccountService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(DepartmentFilterViewModel filterViewModel)
        {
            var currentUser = await _userAccountService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<DepartmentFilterModel>(filterViewModel);
            var users = await _baseUserInfoService.GetForUserAsync(currentUser, filterModel);
            var total = await _baseUserInfoService.CountForUserAsync(currentUser, filterModel);

            await FillAvailableDepartments(currentUser.FacultyId);

            var viewModel = new ItemsViewModel<DepartmentFilterViewModel, BaseUserInfoModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BaseUserInfoModel>(users, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var user = await _userInfoService.GetByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userInfoService.GetByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            await FillAvailableRoles();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateUserViewModel model)
        {
            var existingUser = await _userInfoService.GetByIdAsync(model.Id);
            _mapper.Map(model, existingUser);
            if (ModelState.IsValid)
            {
                await _userInfoService.UpdateAsync(existingUser, User.Identity.GetUserId());
                return RedirectToAction(nameof(Index));
            }

            await FillAvailableRoles();
            return View(existingUser);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await _userInfoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task FillAvailableRoles()
        {
            var currentUser = await _userAccountService.GetByIdAsync(User.Identity.GetUserId());
            ViewBag.AvailableRoles = await _roleService.GetAvailableRolesAsync(currentUser);
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
