using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SRS.Repositories.Context;
using SRS.Web.Models.Account;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using AutoMapper;

namespace SRS.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private readonly IBaseCrudService<AcademicStatusModel> _academicStatusService;
        private readonly IBaseCrudService<ScienceDegreeModel> _scientificDegreeService;
        private readonly IBaseCrudService<PositionModel> _positionService;
        private readonly IUserService<ProfileInfoModel> _userService;
        private readonly IMapper _mapper;

        public ApplicationDbContext db = new ApplicationDbContext();

        public ManageController(
            IBaseCrudService<AcademicStatusModel> academicStatusService,
            IBaseCrudService<ScienceDegreeModel> scientificDegreeService,
            IBaseCrudService<PositionModel> positionService,
            IUserService<ProfileInfoModel> userService,
            IMapper mapper)
        {
            _academicStatusService = academicStatusService;
            _scientificDegreeService = scientificDegreeService;
            _positionService = positionService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                TempData["StatusMessage"] = "Пароль змінено";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("ServerError", "Помилка зміни паролю");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Update()
        {
            await FillRelatedInfo();
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            return View(_mapper.Map<UpdateProfileViewModel>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetByIdAsync(User.Identity.GetUserId());
                _mapper.Map(model, existingUser);
                await _userService.UpdateAsync(existingUser);
                TempData["StatusMessage"] = "Дані оновлено";
                return RedirectToAction(nameof(Index));
            }

            await FillRelatedInfo();
            return View(model);
        }

        private async Task FillRelatedInfo()
        {
            ViewBag.AllAcademicStatuses = await _academicStatusService.GetAllAsync();
            ViewBag.AllScienceDegrees = await _scientificDegreeService.GetAllAsync();
            ViewBag.AllPositions = await _positionService.GetAllAsync();
        }
    }
}