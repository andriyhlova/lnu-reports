using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;
using UserManagement.Models.db;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Працівник, Керівник кафедри, Адміністрація деканату")]
    public class ReportListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportList
        public ActionResult Index(int? page, string dateFrom, string dateTo, int? cathedra)
        {
            db = new ApplicationDbContext();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            ViewBag.cathedra = cathedra ?? 0;
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            List<Report> reports;
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            var isCathedraAdmin = User.IsInRole("Керівник кафедри");
            if (User.IsInRole("Адміністрація деканату"))
            {
                ViewBag.Cathedras = db.Cathedra.Include(x => x.Faculty)
                    .Where(x => x.Faculty.ID == currentUser.Cathedra.Faculty.ID)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.ID.ToString()
                    })
                    .ToList();
                reports = db.Reports.Include(x => x.User.Cathedra.Faculty)
                    .Where(x => (cathedra != null && x.User.Cathedra.ID == cathedra
                        || cathedra == null  && x.User.Cathedra.Faculty.ID == currentUser.Cathedra.Faculty.ID)
                    && (x.User.Id == currentUser.Id 
                        || (x.User.Id != currentUser.Id 
                                && (x.IsConfirmed || (x.IsSigned && isCathedraAdmin && x.User.Cathedra.ID == currentUser.Cathedra.ID)))))
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .OrderByDescending(x => x.Date)
                .ToList();
            }
            else if (isCathedraAdmin)
            {
                reports = db.Reports.Include(x=>x.User.Cathedra).Where(x => (x.User.Cathedra.ID == currentUser.Cathedra.ID)
                && (x.User.Id == currentUser.Id || (x.User.Id != currentUser.Id && x.IsSigned)))
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .OrderByDescending(x=>x.Date)
                .ToList();
            }
            else
            {
                reports = db.Reports.Where(x => x.User.Id == currentUser.Id)
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .OrderByDescending(x => x.Date)
                .ToList();
            }
            return View(reports.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Sign(int reportId)
        {
            var report = db.Reports.Find(reportId);
            report.IsSigned = true;
            db.SaveChanges();
            return RedirectToAction("Index", "ReportList");
        }
        public ActionResult Negate(int reportId)
        {
            var report = db.Reports.Find(reportId);
            report.IsSigned = false;
            report.IsConfirmed = false;
            db.SaveChanges();
            return RedirectToAction("Index", "ReportList");
        }
        public ActionResult Confirm(int reportId)
        {
            var report = db.Reports.Find(reportId);
            report.IsConfirmed = true;
            db.SaveChanges();
            return RedirectToAction("Index", "ReportList");
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
