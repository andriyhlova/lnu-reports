using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.UserModels;
using SRS.Services.Utilities;
using SRS.Web.Identity;
using SRS.Web.Models.Account;

namespace SRS.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IBaseCrudService<CathedraModel> _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<I18nUserInitialsModel> _i18nUserInitialsService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountController(
            IBaseCrudService<CathedraModel> cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<I18nUserInitialsModel> i18nUserInitialsService,
            IEmailService emailService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _i18nUserInitialsService = i18nUserInitialsService;
            _emailService = emailService;
            _mapper = mapper;
        }

        private ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email);
            if (user?.IsActive == false)
            {
                ModelState.AddModelError(string.Empty, "Неактивний користувач");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError(string.Empty, "Неправильні електронна пошта або пароль");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            await AddDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, RoleNames.Worker);
                    await AddUserInitials(user.Id);
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError(string.Empty, "Помилка реєстрації");
            }

            await AddDepartments();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return View(model);
                }

                var hashedGuid = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var recoveryLink = Url.Action(nameof(ResetPassword), "Account", new { code = hashedGuid.Crypt() }, Request.Url.Scheme);
                await _emailService.SendEmail(
                    user.Email,
                    EmailSubjects.PasswordRecovery,
                    $"<a href=\"{recoveryLink}\">Натисніть тут для відновлення паролю</a>");
            }

            return View(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code.Decrypt(), model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            ModelState.AddModelError(string.Empty, "Помилка відновлення паролю");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task AddUserInitials(string userId)
        {
            foreach (var name in Enum.GetNames(typeof(Language)))
            {
                await _i18nUserInitialsService.AddAsync(new I18nUserInitialsModel()
                {
                    Language = (Language)Enum.Parse(typeof(Language), name),
                    UserId = userId
                });
            }
        }

        private async Task AddDepartments()
        {
            ViewBag.AllCathedras = (await _cathedraService.GetAllAsync())
                .OrderBy(x => x.Name)
                .ToList();
            ViewBag.AllFaculties = (await _facultyService.GetAllAsync())
                .OrderBy(x => x.Name)
                .ToList();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}