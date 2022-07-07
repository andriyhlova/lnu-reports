using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UserManagement.Models;
using SRS.Services.Utilities;
using SRS.Domain.Enums;
using SRS.Repositories.Context;
using SRS.Domain.Entities;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Web.Models.Account;

namespace SRS.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db = new ApplicationDbContext();
        private IEmailService _emailService;

        public AccountController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

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
            if (user != null && !user.IsActive)
            {
                ModelState.AddModelError("", "Неактивний користувач");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неправильні електронна пошта або пароль");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.AllCathedras = db.Cathedra.OrderBy(x=> x.Name);
            ViewBag.AllFaculties = db.Faculty.OrderBy(x => x.Name);
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    BirthDate = new DateTime(1950, 1, 1),
                    PublicationCounterBeforeRegistration = 0,
                    MonographCounterBeforeRegistration = 0,
                    BookCounterBeforeRegistration = 0,
                    TrainingBookCounterBeforeRegistration = 0,
                    OtherWritingCounterBeforeRegistration = 0,
                    ConferenceCounterBeforeRegistration = 0,
                    PatentCounterBeforeRegistration = 0
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(UserManager.FindByName(model.Email).Id, "Працівник");
                    var u = db.Users.Where(x => x.UserName == model.Email).First();
                    u.Cathedra = db.Cathedra.Find(model.Cathedra);
                    foreach (var i in Enum.GetNames(typeof(Language)))
                    {
                        u.I18nUserInitials.Add(new I18nUserInitials()
                        {
                            Language = (Language)Enum.Parse(typeof(Language), i),
                            FirstName = "",
                            LastName = "",
                            FathersName = "",
                            User = u,
                        });
                    }
                    db.SaveChanges();
                    TempData["Success"] = true;
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }
            ViewBag.AllCathedras = db.Cathedra.OrderBy(x => x.Name).ToList();
            ViewBag.AllFaculties = db.Faculty.OrderBy(x => x.Name).ToList();
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

            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}