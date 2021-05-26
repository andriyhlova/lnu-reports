using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UserManagement.Models;
using System.Data.Entity.Infrastructure;
using AutoMapper;
using ScientificReport.DAL;
using ScientificReport.DAL.DTO;
using ScientificReport.DAL.Enums;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private ApplicationSignInManager _signInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        private ApplicationUserManager _userManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IManageService manageService;

        public ManageController(IMapper mapper, IManageService manageService):base(mapper)
        {
            this.manageService = manageService;
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(userId),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(userId),
                Logins = await _userManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateViewModel model, int? year, int? GraduationYear, int? DefenseDate, int? AwardingYear, int? AspirantStartYear, int? AspirantFinishYear, int? DoctorStartYear, int? DoctorFinishYear)
        {
            if (ModelState.IsValid)
            {
                var updateDto = mapper.Map<UpdateViewModel, UpdateDTO>(model);
                var currentUserId = User.Identity.GetUserId();
                 var user = manageService.UpdateUser(updateDto, currentUserId, year, GraduationYear, DefenseDate, AwardingYear,
                    AspirantStartYear, AspirantFinishYear, DoctorStartYear, DoctorFinishYear);

                ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");
                ViewBag.AwardingDate = user.AwardingDate?.ToString("yyyy-MM-dd");
                ViewBag.GraduationDate = user.GraduationDate?.ToString("yyyy-MM-dd");
                ViewBag.DefenseYear = user.DefenseYear?.ToString("yyyy-MM-dd");
                return RedirectToAction("Index", "Manage");
            }

            ViewBag.AllAcademicStatuses = manageService.GetAcademicStatuses();
            ViewBag.AllScienceDegrees = manageService.GetScienceDegrees();
            ViewBag.AllPositions = manageService.GetPositions();
            return View(model);
        }
        
        public ActionResult Update()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            ViewBag.AllAcademicStatuses = manageService.GetAcademicStatuses();
            ViewBag.AllScienceDegrees = manageService.GetScienceDegrees();
            ViewBag.AllPositions = manageService.GetPositions();
            ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");
            ViewBag.AwardingDate = user.AwardingDate?.ToString("yyyy");
            ViewBag.GraduationDate = user.GraduationDate?.ToString("yyyy");
            ViewBag.DefenseYear = user.DefenseYear?.ToString("yyyy");
            ViewBag.AspirantStartYear = user.AspirantStartYear?.ToString("yyyy");
            ViewBag.AspirantFinishYear = user.AspirantFinishYear?.ToString("yyyy");
            ViewBag.DoctorStartYear = user.DoctorStartYear?.ToString("yyyy");
            ViewBag.DoctorFinishYear = user.DoctorFinishYear?.ToString("yyyy");
            ViewBag.PublicationsBeforeRegistration = user.PublicationCounterBeforeRegistration;
            ViewBag.MonographCounterBeforeRegistration = user.MonographCounterBeforeRegistration;
            ViewBag.BookCounterBeforeRegistration = user.BookCounterBeforeRegistration;
            ViewBag.TrainingBookCounterBeforeRegistration = user.TrainingBookCounterBeforeRegistration;
            ViewBag.OtherWritingCounterBeforeRegistration = user.OtherWritingCounterBeforeRegistration;
            ViewBag.ConferenceCounterBeforeRegistration = user.ConferenceCounterBeforeRegistration;
            ViewBag.PatentCounterBeforeRegistration = user.PatentCounterBeforeRegistration;
            if (user.AcademicStatus != null)
                ViewBag.AcademicStatus = user.AcademicStatus.Value.ToString();
            if (user.ScienceDegree != null)
                ViewBag.ScienceDegree = user.ScienceDegree.Value.ToString();
            if (user.Position != null)
                ViewBag.Position = user.Position.Value.ToString();
            var viewModel = new UpdateViewModel()
            {
                Email = user.Email,
                I18nUserInitials = user.I18nUserInitials,
            };
            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

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

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        #endregion
    }
}