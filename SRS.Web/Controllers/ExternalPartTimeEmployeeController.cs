using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.UserModels;
using SRS.Web.Identity;
using SRS.Web.Models.Account;
using SRS.Web.Models.ThemeOfScientificWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SRS.Web.Controllers
{
    public class ExternalPartTimeEmployeeController : Controller
    {
        private readonly IBaseCrudService<CathedraModel> _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<I18nUserInitialsModel> _i18nUserInitialsService;
        private readonly IMapper _mapper;

        public ExternalPartTimeEmployeeController(
            IBaseCrudService<CathedraModel> cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<I18nUserInitialsModel> i18nUserInitialsService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _i18nUserInitialsService = i18nUserInitialsService;
            _mapper = mapper;
        }

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Create()
        {
            await AddDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Create(ExternalPartTimeEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user.IsActive = true;
                    await UserManager.AddToRoleAsync(user.Id, RoleNames.Worker);
                    await UserManager.AddToRoleAsync(user.Id, RoleNames.ExternalPartTimeEmployee);
                    await AddUserInitials(user.Id);
                    TempData["Success"] = true;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Помилка створення сумісника");
            }

            await AddDepartments();
            return View(model);
        }

        private async Task AddUserInitials(string userId)
        {
            var availableLanguages = Enum.GetNames(typeof(Language)).Where(x => x == Language.UA.ToString() || x == Language.EN.ToString());
            foreach (var name in availableLanguages)
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
    }
}