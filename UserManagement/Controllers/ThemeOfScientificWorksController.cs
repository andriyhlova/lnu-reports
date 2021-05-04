using PagedList;
using ScientificReport.DAL;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using UserManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Controllers
{
  [Authorize(Roles = "Керівник кафедри")]
  public class ThemeOfScientificWorksController : Controller {
    private IThemeOfScientificWorksService themeOfScientificWorksService = new ThemeOfScientificWorksService();
    private IUserService userService = new UserService();
    private ICathedraService cathedraService = new CathedraService();

    // GET: ThemeOfScientificWorks
    public async Task<ActionResult> Index(int? page) {
      int pageSize = 15;
      int pageNumber = (page ?? 1);
      var user = userService.GetCurrentUser(User.Identity.Name);
      var scientifthemes = await themeOfScientificWorksService.GetScientificThemes(user.Cathedra.ID);
      return View(scientifthemes.ToPagedList(pageNumber, pageSize));
    }

    // GET: ThemeOfScientificWorks/Details/5
    public async Task<ActionResult> Details(int? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ThemeOfScientificWork themeOfScientificWork = await themeOfScientificWorksService.GetScientificThemeById(id);
      if (themeOfScientificWork == null) {
        return HttpNotFound();
      }
      return View(themeOfScientificWork);
    }

    // GET: ThemeOfScientificWorks/Create
    public ActionResult Create() {
      ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
          .Select(x => new SelectListItem { Selected = false, Text = x.ToLower().Replace('_', ' '), Value = x }).ToList();
      return View();
    }

    // POST: ThemeOfScientificWorks/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "ID,Value,ScientificHead,PeriodFrom,PeriodTo,Financial,ThemeNumber,Code")] ThemeOfScientificWork themeOfScientificWork) {
      if (ModelState.IsValid) {
        var user = userService.GetCurrentUser(User.Identity.Name);
        themeOfScientificWork.Cathedra = cathedraService.GetCathedraById(user.Cathedra.ID);
        await themeOfScientificWorksService.AddTheme(themeOfScientificWork);
        return RedirectToAction("Index");
      }

      return View(themeOfScientificWork);
    }

    // GET: ThemeOfScientificWorks/Edit/5
    public async Task<ActionResult> Edit(int? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ThemeOfScientificWork themeOfScientificWork = await themeOfScientificWorksService.GetScientificThemeById(id);
      ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
          .Select(x => new SelectListItem { Selected = false, Text = x.ToLower(), Value = x }).ToList();
      if (themeOfScientificWork == null) {
        return HttpNotFound();
      }
      return View(themeOfScientificWork);
    }

    // POST: ThemeOfScientificWorks/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "ID,Value,ScientificHead,PeriodFrom,PeriodTo,ThemeNumber,Financial,Code")] ThemeOfScientificWork themeOfScientificWork) {
      if (ModelState.IsValid) {
        await themeOfScientificWorksService.SetStateModified(themeOfScientificWork);
        return RedirectToAction("Index");
      }
      return View(themeOfScientificWork);
    }

    // GET: ThemeOfScientificWorks/Delete/5
    public async Task<ActionResult> Delete(int? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ThemeOfScientificWork themeOfScientificWork = await themeOfScientificWorksService.GetScientificThemeById(id);
      if (themeOfScientificWork == null) {
        return HttpNotFound();
      }
      return View(themeOfScientificWork);
    }

    // POST: ThemeOfScientificWorks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id) {
      await themeOfScientificWorksService.RemoveTheme(id);
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        themeOfScientificWorksService.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
