using System.Collections.Generic;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction
{
    public interface IReportCathedraListService
    {
        IEnumerable<CathedraReport> GetReports(string currentUserId, string dateFrom, string dateTo);
    }
}