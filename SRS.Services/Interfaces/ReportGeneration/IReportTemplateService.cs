using System.Threading.Tasks;
using SRS.Services.Models.ReportGenerationModels.Report;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface IReportTemplateService
    {
        Task<ReportTemplateModel> BuildAsync(int reportId);
    }
}
