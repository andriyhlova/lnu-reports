using PagedList;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Repositories.Context;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManagement.Extensions;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри, Адміністрація деканату")]
    public class ThemeOfScientificWorksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IBaseCrudService<ThemeOfScientificWorkModel> _themeOfScientificWorkService;

        public ThemeOfScientificWorksController(IBaseCrudService<ThemeOfScientificWorkModel> themeOfScientificWorkService)
        {
            _themeOfScientificWorkService = themeOfScientificWorkService;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var user = db.Users.Include(x=>x.Roles)
                .Where(x => x.UserName == User.Identity.Name).First();
            var roles = db.Roles.ToList();
            var cathedraAdmin = roles.FirstOrDefault(x => x.Name == "Керівник кафедри");
            var facultyAdmin = roles.FirstOrDefault(x => x.Name == "Адміністрація деканату");
            var scientifthemes = new List<ThemeOfScientificWork>();
            if (user.Roles.Any(x=>x.RoleId == facultyAdmin.Id))
            {
                scientifthemes = db.ThemeOfScientificWork.Include(x=>x.Cathedra.Faculty)
                    .Where(x => x.Cathedra.Faculty.Id == user.Cathedra.Faculty.Id)
                    .OrderByDescending(x=>x.PeriodTo)
                    .ToList();
            }
            else if(user.Roles.Any(x=>x.RoleId == cathedraAdmin.Id))
            {
                scientifthemes = db.ThemeOfScientificWork.Include(x => x.Cathedra)
                    .Where(x => x.Cathedra.Id == user.Cathedra.Id)
                    .OrderByDescending(x => x.PeriodTo)
                    .ToList();
            }
            return View(scientifthemes.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create([Bind(Include = "Id,Value,ScientificHead,PeriodFrom,PeriodTo,Financial,ThemeNumber,Code")] ThemeOfScientificWork themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.UserName == User.Identity.Name).First();
                themeOfScientificWork.Cathedra = db.Cathedra.Where(x => x.Id == user.Cathedra.Id).First();
                db.ThemeOfScientificWork.Add(themeOfScientificWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillFinancials()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.GetFriendlyName(), Value = x })
                .ToList();
        }
    }
}
