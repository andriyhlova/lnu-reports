using PagedList;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри, Адміністрація деканату")]
    public class ThemeOfScientificWorksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ThemeOfScientificWorks
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

        // GET: ThemeOfScientificWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Create
        public ActionResult Create()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.ToLower().Replace('_', ' '), Value = x }).ToList();
            return View();
        }

        // POST: ThemeOfScientificWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: ThemeOfScientificWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.ToLower(), Value = x }).ToList();
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // POST: ThemeOfScientificWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value,ScientificHead,PeriodFrom,PeriodTo,ThemeNumber,Financial,Code")] ThemeOfScientificWork themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(themeOfScientificWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // POST: ThemeOfScientificWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            db.ThemeOfScientificWork.Remove(themeOfScientificWork);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
