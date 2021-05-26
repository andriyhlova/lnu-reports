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
    public class ReportListService : IReportListService
    {
        private IUnitOfWork db;
        public ReportListService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Report> GetReportsForCathedraManager(string currentUserId, string dateFrom, string dateTo)
        {
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            var currentUser = db.Users.GetAllAsync().Result.First(u => u.Id == currentUserId);
            return db.Reports.GetAllAsync().Result.Where(x => (x.User.Cathedra.Id == currentUser.Cathedra.Id)
                                            && (x.User.Id == currentUser.Id || (x.User.Id != currentUser.Id && x.IsSigned)))
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
        }

        public IEnumerable<Report> GetReports(string currentUserId, string dateFrom, string dateTo)
        {
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            var currentUser = db.Users.GetAllAsync().Result.First(u => u.Id == currentUserId);
            return db.Reports.GetAllAsync().Result.Where(x => x.User.Id == currentUser.Id)
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
        }

        public async void ConfirmReport(int reportId)
        {
            var report = await db.Reports.FindByIdAsync(reportId);
            report.IsConfirmed = true;
            db.SaveChanges();
        }

        public async void SignReport(int reportId)
        {
            var report = await db.Reports.FindByIdAsync(reportId);
            report.IsSigned = true;
            db.SaveChanges();
        }

        public async void NegateReport(int reportId)
        {
            var report = await db.Reports.FindByIdAsync(reportId);
            report.IsSigned = false;
            report.IsConfirmed = false;
            db.SaveChanges();
        }
    }
}
