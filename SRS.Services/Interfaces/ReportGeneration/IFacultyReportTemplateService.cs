using SRS.Services.Models.ReportGenerationModels.DepartmentReport;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface IFacultyReportTemplateService
    {
        Task<DepartmentReportTemplateModel> BuildAsync(int reportId);
    }
}
