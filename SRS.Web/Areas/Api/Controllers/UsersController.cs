using Microsoft.AspNet.Identity;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Services.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IUserService<UserInitialsModel> _userInitialsService;

        public UsersController(IUserService<UserAccountModel> userService, IUserService<UserInitialsModel> userInitialsService)
        {
            _userService = userService;
            _userInitialsService = userInitialsService;
        }

        [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
        [HttpGet]
        public async Task<ActionResult> GetByFacultyAndCathedra(int? facultyId, int? cathedraId, string roleId, bool? isActive)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var users = await _userInitialsService.GetForUserAsync(user, new UserFilterModel { FacultyId = facultyId, CathedraId = cathedraId, RoleId = roleId, OrderBy = (int)UserOrderType.LastName, IsActive = isActive });
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> SearchAll(string search)
        {
            var users = await _userInitialsService.GetForUserAsync(
                new UserAccountModel
                {
                    RoleIds = new List<string> { RolesProvider.AllRoles.FirstOrDefault(x => x.Value == RoleNames.Superadmin).Key }
                },
                new UserFilterModel { Search = search, OrderBy = (int)UserOrderType.LastName });
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
