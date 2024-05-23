using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Identity;
using SRS.Web.Models.Account;

namespace SRS.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IUserService<ProfileInfoModel> _userService;
        private readonly IMapper _mapper;

        public ManageController(
            IPositionService positionService,
            IUserService<ProfileInfoModel> userService,
            IMapper mapper)
        {
            _positionService = positionService;
            _userService = userService;
            _mapper = mapper;
        }

        private ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

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
            ViewBag.AllPositions = await _positionService.GetAllAsync(new BaseFilterModel());
        }
    }
}