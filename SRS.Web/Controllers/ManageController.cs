using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ApplicationDbContext db = new ApplicationDbContext();

        public ManageController(
            IBaseCrudService<AcademicStatusModel> academicStatusService,
            IBaseCrudService<ScienceDegreeModel> scientificDegreeService,
            IBaseCrudService<PositionModel> positionService,
            IUserService userService,
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

            ModelState.AddModelError("", "Помилка зміни паролю");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Update()
        {
            await FillRelatedInfo();
            var user = await _userService.GetUserInfoByIdAsync(User.Identity.GetUserId());
            return View(_mapper.Map<UpdateViewModel>(user));

            //var user = UserManager.FindById(User.Identity.GetUserId());
            //ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");
            //ViewBag.AwardingDate = user.AwardingDate?.ToString("yyyy");
            //ViewBag.GraduationDate = user.GraduationDate?.ToString("yyyy");
            //ViewBag.DefenseYear = user.DefenseYear?.ToString("yyyy");
            //ViewBag.AspirantStartYear = user.AspirantStartYear?.ToString("yyyy");
            //ViewBag.AspirantFinishYear = user.AspirantFinishYear?.ToString("yyyy");
            //ViewBag.DoctorStartYear = user.DoctorStartYear?.ToString("yyyy");
            //ViewBag.DoctorFinishYear = user.DoctorFinishYear?.ToString("yyyy");
            //ViewBag.PublicationsBeforeRegistration = user.PublicationCounterBeforeRegistration;
            //ViewBag.MonographCounterBeforeRegistration = user.MonographCounterBeforeRegistration;
            //ViewBag.BookCounterBeforeRegistration = user.BookCounterBeforeRegistration;
            //ViewBag.TrainingBookCounterBeforeRegistration = user.TrainingBookCounterBeforeRegistration;
            //ViewBag.OtherWritingCounterBeforeRegistration = user.OtherWritingCounterBeforeRegistration;
            //ViewBag.ConferenceCounterBeforeRegistration = user.ConferenceCounterBeforeRegistration;
            //ViewBag.PatentCounterBeforeRegistration = user.PatentCounterBeforeRegistration;
            //if (user.AcademicStatus != null)
            //    ViewBag.AcademicStatus = user.AcademicStatus.Value.ToString();
            //if (user.ScienceDegree != null)
            //    ViewBag.ScienceDegree = user.ScienceDegree.Value.ToString();
            //if (user.Position != null)
            //    ViewBag.Position = user.Position.Value.ToString();
            //var viewModel = new UpdateViewModel()
            //{
            //    Email = user.Email,
            //    I18nUserInitials = user.I18nUserInitials,
            //};
            //return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userModel = _mapper.Map<UserInfoModel>(model);
                await _userService.UpdateAsync(userModel);
                TempData["StatusMessage"] = "Дані оновлено";
                return RedirectToAction(nameof(Index));
            }

            await FillRelatedInfo();
            return View(model);
            //if (ModelState.IsValid)
            //{
            //    var currentUserId = User.Identity.GetUserId();

            //    var user1 = await _userService.GetUserInfoByIdAsync(User.Identity.GetUserId());
                

            //    var user = db.Users.First(x => x.Id == currentUserId);
            //    user.BirthDate = model.BirthDate;
            //    user.AwardingDate = AwardingYear.HasValue ? new DateTime(AwardingYear.Value, 1, 1) : (DateTime?)null;
            //    user.GraduationDate = GraduationYear.HasValue ? new DateTime(GraduationYear.Value, 1, 1) : (DateTime?)null;
            //    user.DefenseYear = DefenseDate.HasValue ? new DateTime(DefenseDate.Value, 1, 1) : (DateTime?)null;
            //    user.AspirantStartYear = AspirantStartYear != null ? new DateTime(AspirantStartYear.Value, 1, 1) : (DateTime?)null;
            //    user.AspirantFinishYear = AspirantFinishYear != null ? new DateTime(AspirantFinishYear.Value, 1, 1) : (DateTime?)null;
            //    user.DoctorStartYear = DoctorStartYear != null ? new DateTime(DoctorStartYear.Value, 1, 1) : (DateTime?)null;
            //    user.DoctorFinishYear = DoctorFinishYear != null ? new DateTime(DoctorFinishYear.Value, 1, 1) : (DateTime?)null;
                
            //    user.PublicationCounterBeforeRegistration = model.PublicationsBeforeRegistration;
            //    user.MonographCounterBeforeRegistration = model.MonographCounterBeforeRegistration;
            //    user.BookCounterBeforeRegistration = model.BookCounterBeforeRegistration;
            //    user.TrainingBookCounterBeforeRegistration = model.TrainingBookCounterBeforeRegistration;
            //    user.OtherWritingCounterBeforeRegistration = model.OtherWritingCounterBeforeRegistration;
            //    user.ConferenceCounterBeforeRegistration = model.ConferenceCounterBeforeRegistration;
            //    user.PatentCounterBeforeRegistration = model.PatentCounterBeforeRegistration;

            //    user.AcademicStatus = db.AcademicStatus.First(x => x.Value == model.AcademicStatus);
            //    user.ScienceDegree = db.ScienceDegree.First(x => x.Value == model.ScienceDegree);
            //    user.Position = db.Position.First(x => x.Value == model.Position);
            //    var intials = user.I18nUserInitials.ToList();
            //    foreach (var initial in intials)
            //    {
            //        db.Entry(initial).State = EntityState.Deleted;
            //    }
            //    user.I18nUserInitials = model.I18nUserInitials;
            //    db.SaveChanges();

            //    ViewBag.BirthDate = user.BirthDate.ToString("yyyy-MM-dd");
            //    ViewBag.AwardingDate = user.AwardingDate?.ToString("yyyy-MM-dd");
            //    ViewBag.GraduationDate = user.GraduationDate?.ToString("yyyy-MM-dd");
            //    ViewBag.DefenseYear = user.DefenseYear?.ToString("yyyy-MM-dd");
            //    return RedirectToAction("Index", "Manage");
            //}

            //await FillRelatedInfo();
            //return View(model);
        }

        private async Task FillRelatedInfo()
        {
            ViewBag.AllAcademicStatuses = await _academicStatusService.GetAllAsync();
            ViewBag.AllScienceDegrees = await _scientificDegreeService.GetAllAsync();
            ViewBag.AllPositions = await _positionService.GetAllAsync();
        }
    }
}