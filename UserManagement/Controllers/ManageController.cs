﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UserManagement.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using SRS.Repositories.Context;

namespace UserManagement.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db = new ApplicationDbContext();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
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
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
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
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
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
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
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
                var currentUserId = User.Identity.GetUserId();
                var user = db.Users.First(x => x.Id == currentUserId);
                user.BirthDate = model.BirthDate;
                user.AwardingDate = AwardingYear.HasValue ? new DateTime(AwardingYear.Value, 1, 1) : (DateTime?)null;
                user.GraduationDate = GraduationYear.HasValue ? new DateTime(GraduationYear.Value, 1, 1) : (DateTime?)null;
                user.DefenseYear = DefenseDate.HasValue ? new DateTime(DefenseDate.Value, 1, 1) : (DateTime?)null;
                user.AspirantStartYear = AspirantStartYear != null ? new DateTime(AspirantStartYear.Value, 1, 1) : (DateTime?)null;
                user.AspirantFinishYear = AspirantFinishYear != null ? new DateTime(AspirantFinishYear.Value, 1, 1) : (DateTime?)null;
                user.DoctorStartYear = DoctorStartYear != null ? new DateTime(DoctorStartYear.Value, 1, 1) : (DateTime?)null;
                user.DoctorFinishYear = DoctorFinishYear != null ? new DateTime(DoctorFinishYear.Value, 1, 1) : (DateTime?)null;
                
                user.PublicationCounterBeforeRegistration = model.PublicationsBeforeRegistration;
                user.MonographCounterBeforeRegistration = model.MonographCounterBeforeRegistration;
                user.BookCounterBeforeRegistration = model.BookCounterBeforeRegistration;
                user.TrainingBookCounterBeforeRegistration = model.TrainingBookCounterBeforeRegistration;
                user.OtherWritingCounterBeforeRegistration = model.OtherWritingCounterBeforeRegistration;
                user.ConferenceCounterBeforeRegistration = model.ConferenceCounterBeforeRegistration;
                user.PatentCounterBeforeRegistration = model.PatentCounterBeforeRegistration;

                user.AcademicStatus = db.AcademicStatus.First(x => x.Value == model.AcademicStatus);
                user.ScienceDegree = db.ScienceDegree.First(x => x.Value == model.ScienceDegree);
                user.Position = db.Position.First(x => x.Value == model.Position);
                var intials = user.I18nUserInitials.ToList();
                foreach (var initial in intials)
                {
                    db.Entry(initial).State = EntityState.Deleted;
                }
                user.I18nUserInitials = model.I18nUserInitials;
                db.SaveChanges();

                ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");
                ViewBag.AwardingDate = user.AwardingDate?.ToString("yyyy-MM-dd");
                ViewBag.GraduationDate = user.GraduationDate?.ToString("yyyy-MM-dd");
                ViewBag.DefenseYear = user.DefenseYear?.ToString("yyyy-MM-dd");
                return RedirectToAction("Index", "Manage");
            }
            
            ViewBag.AllAcademicStatuses = db.AcademicStatus.ToList().Select(x => x.Value);
            ViewBag.AllScienceDegrees = db.ScienceDegree.ToList().Select(x => x.Value);
            ViewBag.AllPositions = db.Position.ToList().Select(x => x.Value);
            return View(model);
        }
        
        public ActionResult Update()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            ViewBag.AllAcademicStatuses = db.AcademicStatus.ToList().Select(x => x.Value);
            ViewBag.AllScienceDegrees = db.ScienceDegree.ToList().Select(x => x.Value);
            ViewBag.AllPositions = db.Position.ToList().Select(x => x.Value);
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
                _userManager = null;
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
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}