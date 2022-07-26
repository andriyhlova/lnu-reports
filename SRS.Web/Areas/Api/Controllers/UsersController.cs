using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Services.Providers;

namespace UserManagement.Areas.Api.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    public class UsersController : Controller
    {
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IUserService<UserInitialsModel> _userInitialsService;

        public UsersController(IUserService<UserAccountModel> userService, IUserService<UserInitialsModel> userInitialsService)
        {
            _userService = userService;
            _userInitialsService = userInitialsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetByFacultyAndCathedra(int? facultyId, int? cathedraId)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var users = await _userInitialsService.GetForUserAsync(user, new DepartmentFilterModel { FacultyId = facultyId, CathedraId = cathedraId });
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
                new DepartmentFilterModel { Search = search });
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
