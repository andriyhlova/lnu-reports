using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Entities;
using SRS.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри, Адміністрація деканату")]
    public class ReportCathedraListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportList
        public ActionResult Index(int? page, string dateFrom, string dateTo)
        {
            db = new ApplicationDbContext();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            List<CathedraReport> reports = new List<CathedraReport>();
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            if (User.IsInRole("Адміністрація деканату"))
            {
                reports = db.CathedraReport.Include(x => x.User.Cathedra.Faculty)
                    .Where(x => x.User.Cathedra.Faculty.ID == currentUser.Cathedra.Faculty.ID)
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
            }
            else if (User.IsInRole("Керівник кафедри"))
            {
                reports = db.CathedraReport.Include(x => x.User.Cathedra)
                    .Where(x => x.User.Cathedra.ID == currentUser.Cathedra.ID)
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
            }
            return View(reports.ToPagedList(pageNumber, pageSize));
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
