using System.Threading.Tasks;
using SRS.Services.Models.ReportGenerationModels.DepartmentReport;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface ICathedraReportTemplateService
    {
        Task<DepartmentReportTemplateModel> BuildAsync(int reportId);
    }
}
