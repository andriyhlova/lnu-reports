using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.ReportModels;

namespace SRS.Services.Implementations
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IBaseRepository<Report> _repo;

        public ReportTemplateService(IBaseRepository<Report> repo)
        {
            _repo = repo;
        }

        public async Task<ReportTemplateModel> BuildAsync(int reportId)
        {
            var dbReport = await _repo.GetAsync(reportId);
            var report = new ReportTemplateModel();
            report.Year = dbReport.Date?.Year ?? 0;
            return await Task.FromResult(report);
        }
    }
}
