using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
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
        private readonly IPositionService _positionService;
        private readonly ICsvService _csvService;
        private readonly IMapper _mapper;

        public UsersManagementController(
            IBaseCrudService<CathedraModel> cathedraCrudService,
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IUserService<BaseUserInfoModel> baseUserInfoService,
            IUserService<UserInfoModel> userInfoService,
            IUserService<UserAccountModel> userAccountService,
            IRoleService roleService,
            IPositionService positionService,
            ICsvService csvService,
            IMapper mapper)
        {
            _cathedraCrudService = cathedraCrudService;
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _baseUserInfoService = baseUserInfoService;
            _userInfoService = userInfoService;
            _userAccountService = userAccountService;
            _roleService = roleService;
            _positionService = positionService;
            _csvService = csvService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(UserFilterViewModel filterViewModel)
        {
            var currentUser = await _userAccountService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<UserFilterModel>(filterViewModel);
            var users = await _baseUserInfoService.GetForUserAsync(currentUser, filterModel);
            var total = await _baseUserInfoService.CountForUserAsync(currentUser, filterModel);

            await FillAvailableDepartments(currentUser.FacultyId);

            var viewModel = new ItemsViewModel<UserFilterViewModel, BaseUserInfoModel>
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

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(UserFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<UserFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var currentUser = await _userAccountService.GetByIdAsync(User.Identity.GetUserId());
            var users = await _baseUserInfoService.GetForUserAsync(currentUser, filterModel);
            var csvModel = new CsvModel<BaseUserInfoCsvModel>
            {
                Data = _mapper.Map<IList<BaseUserInfoCsvModel>>(users)
            };

            byte[] fileBytes = _csvService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "users.csv");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userInfoService.GetByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            await FillRelatedEntities();
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

            await FillRelatedEntities();
            return View(existingUser);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
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

        private async Task FillRelatedEntities()
        {
            var currentUser = await _userAccountService.GetByIdAsync(User.Identity.GetUserId());
            await FillAvailableRoles(currentUser);
            await FillAvailableDepartments(currentUser.FacultyId);
            await FillAvailablePositions();
        }

        private async Task FillAvailableRoles(UserAccountModel currentUser)
        {
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

        private async Task FillAvailablePositions()
        {
            ViewBag.AllPositions = await _positionService.GetAllAsync(new BaseFilterModel());
        }
    }
}
