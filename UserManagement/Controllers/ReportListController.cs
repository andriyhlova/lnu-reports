using Microsoft.AspNet.Identity;
using PagedList;
using ScientificReport.DAL;
using ScientificReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScientificReport.DAL.Configuration;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Працівник, Керівник кафедри")]
    public class ReportListController : Controller
    {
       private readonly IReportListService _reportListService;
       public ReportListController(IReportListService reportListService)
       {
           this._reportListService = reportListService;
       }
        // GET: ReportList
        public ActionResult Index(int? page, string dateFrom, string dateTo)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            var currentUserId = User.Identity.GetUserId();
            var reports = User.IsInRole(Roles.CathedraManager)
                ? _reportListService.GetReportsForCathedraManager(currentUserId, dateFrom, dateTo).ToList()
                : _reportListService.GetReports(currentUserId, dateFrom, dateTo).ToList();
            return View(reports.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Sign(int reportId)
        {
            _reportListService.SignReport(reportId);
            return RedirectToAction("Index", "ReportList");
        }
        public ActionResult Negate(int reportId)
        {
            _reportListService.NegateReport(reportId);
            return RedirectToAction("Index", "ReportList");
        }
        public ActionResult Confirm(int reportId)
        {
            _reportListService.ConfirmReport(reportId);
            return RedirectToAction("Index", "ReportList");
        }
    }
}
