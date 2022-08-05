using System.Threading.Tasks;
using SRS.Services.Models.ReportGenerationModels;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface IReportTemplateService
    {
        Task<ReportTemplateModel> BuildAsync(int reportId);
    }
}
