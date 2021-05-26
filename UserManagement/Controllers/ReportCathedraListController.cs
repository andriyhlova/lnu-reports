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
using ScientificReport.Services.Abstraction;
using UserManagement.Models.Reports;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class ReportCathedraListController : Controller
    {
        private readonly IReportCathedraListService _reportCathedraListService;
        public ReportCathedraListController(IReportCathedraListService reportCathedraListService)
        {
            this._reportCathedraListService = reportCathedraListService;
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
            var reports = _reportCathedraListService.GetReports(currentUserId, dateFrom, dateTo);
            return View(reports.ToPagedList(pageNumber, pageSize));
        }
    }
}
