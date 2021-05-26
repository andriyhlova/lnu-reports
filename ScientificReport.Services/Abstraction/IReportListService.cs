using System.Collections.Generic;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction
{
    public interface IReportListService
    {
        IEnumerable<Report> GetReportsForCathedraManager(string currentUserId, string dateFrom, string dateTo);
        IEnumerable<Report> GetReports(string currentUserId, string dateFrom, string dateTo);
        void ConfirmReport(int reportId);
        void SignReport(int reportId);
        void NegateReport(int reportId);
    }
}