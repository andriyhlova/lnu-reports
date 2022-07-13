using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManagement.Extensions;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Керівник кафедри, Адміністрація деканату")]
    public class ThemeOfScientificWorksController : Controller
    {
        private IThemeOfScientificWorkService _themeOfScientificWorkService;
        private IUserService<UserAccountModel> _userService;

        public ThemeOfScientificWorksController(IThemeOfScientificWorkService themeOfScientificWorkService, IUserService<UserAccountModel> userService)
        {
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? page = 1)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var scientifthemes = await _themeOfScientificWorkService.GetThemesForUserAsync(user, (page.Value - 1) * PaginationValues.PageSize, PaginationValues.PageSize);
            var total = await _themeOfScientificWorkService.CountThemesForUserAsync(user);
            return View(new StaticPagedList<ThemeOfScientificWorkModel>(scientifthemes, page.Value, PaginationValues.PageSize, total));
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var themeOfScientificWork = await _themeOfScientificWorkService.GetAsync(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpGet]
        public ActionResult Create()
        {
            FillFinancials();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
                themeOfScientificWork.CathedraId = user.CathedraId;
                await _themeOfScientificWorkService.AddAsync(themeOfScientificWork);
                return RedirectToAction(nameof(Index));
            }

            FillFinancials();
            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var themeOfScientificWork = await _themeOfScientificWorkService.GetAsync(id);
            FillFinancials();
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkService.UpdateAsync(themeOfScientificWork);
                return RedirectToAction(nameof(Index));
            }

            FillFinancials();
            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var themeOfScientificWork = await _themeOfScientificWorkService.GetAsync(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _themeOfScientificWorkService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private void FillFinancials()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Text = x.GetFriendlyName(), Value = x })
                .ToList();
        }
    }
}
