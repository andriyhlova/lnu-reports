using System.Threading.Tasks;
using SRS.Services.Models.ReportModels;

namespace SRS.Services.Interfaces
{
    public interface IReportTemplateService
    {
        Task<ReportTemplateModel> BuildAsync(int reportId);
    }
}
