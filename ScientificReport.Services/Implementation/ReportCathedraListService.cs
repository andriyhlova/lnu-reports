using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.DTO;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace ScientificReport.Services.Implementation
{
    public class ReportCathedraListService : IReportCathedraListService
    {
        private IUnitOfWork db;
        public ReportCathedraListService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<CathedraReport> GetReports(string currentUserId, string dateFrom, string dateTo)
        {
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            var currentUser = db.Users.GetAllAsync().Result.First(u => u.Id == currentUserId);
            List<CathedraReport> reports;
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            reports = db.CathedraReports.GetAllAsync().Result.Where(x => x.User.Cathedra.Id == currentUser.Cathedra.Id)
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
            return reports;
        }
    }
}
