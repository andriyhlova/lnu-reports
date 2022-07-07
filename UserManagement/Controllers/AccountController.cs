using System;
using System.Configuration;
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
using SRS.Services.Implementations;

namespace UserManagement.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db = new ApplicationDbContext();
        private SRS.Services.Implementations.EmailService emailService;
        public AccountController()
        {
            emailService = new SRS.Services.Implementations.EmailService();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Success = TempData["Success"];
           
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = UserManager.FindByEmail(model.Email);
            if (user != null && !user.IsActive)
            {
                ModelState.AddModelError("", "This user is not active.");
                return View(model);
            }

            HttpCookie cookie = new HttpCookie("UserName", model.Email);
            HttpCookie cookie1 = HttpContext.Request.Cookies.Get("UserName");
            if (model.RememberMe)
            {
                cookie.Expires = DateTime.Now.AddDays(10);
            }
            Response.Cookies.Add(cookie);

            


            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
          
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.AllCathedras = db.Cathedra.OrderBy(x=> x.Name);
            ViewBag.AllFaculties = db.Faculty.OrderBy(x => x.Name);
            
            return View();
        }

        //
        // POST: /Account/Register
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
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
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

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
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

                // get change password page


                //var provider = new DpapiDataProtectionProvider("SampleAppName");

                //UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                //    provider.Create("SampleTokenName"));

                string hashedGuId = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                hashedGuId = hashedGuId.Crypt();

                string recoveryLink = Request.Url.ToString()
                    .Replace("ForgotPassword", $"ResetPassword?code=" + hashedGuId );
                emailService.SendEmail(user.Email, "Відновлення паролю",
                    $"<a href=\"{recoveryLink}\">Натисніть тут для відновлення паролю</a>");
            }
            return View("ForgotPasswordConfirmation");
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
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
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }            

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code.Decrypt(), model.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
            return RedirectToAction("Index", "Home");
        }
        #endregion

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