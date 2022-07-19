using Microsoft.AspNet.Identity;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    [RoutePrefix("api/users")]
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
            var users = await _userInitialsService.GetForUserAsync(user, facultyId, cathedraId);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
