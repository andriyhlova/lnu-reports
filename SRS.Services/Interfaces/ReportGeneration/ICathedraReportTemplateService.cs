using System.Threading.Tasks;
using SRS.Services.Models.ReportGenerationModels.CathedraReport;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface ICathedraReportTemplateService
    {
        Task<CathedraReportTemplateModel> BuildAsync(int reportId);
    }
}
