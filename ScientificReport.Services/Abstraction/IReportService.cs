using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction
{
    public interface IReportService
    {
        String GenerateHTMLReport(int reportId);
        String GenerateHTMLReport(Report report);
    }
}
