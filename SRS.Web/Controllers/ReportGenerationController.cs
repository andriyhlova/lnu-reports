using Rotativa;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.Constants;
using SRS.Services.Models.ReportGenerationModels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    public class ReportGenerationController : Controller
    {
        private readonly IReportTemplateService _reportTemplateService;
        private readonly IHtmlReportBuilderService<ReportTemplateModel> _htmlReportBuilderService;
        private readonly ITexReportBuilderService _texReportBuilderService;

        public ReportGenerationController(
            IReportTemplateService reportTemplateService,
            IHtmlReportBuilderService<ReportTemplateModel> htmlReportBuilderService,
            ITexReportBuilderService texReportBuilderService)
        {
            _reportTemplateService = reportTemplateService;
            _htmlReportBuilderService = htmlReportBuilderService;
            _texReportBuilderService = texReportBuilderService;
        }

        [HttpGet]
        public async Task<ActionResult> Preview(int reportId)
        {
            var model = await _reportTemplateService.BuildAsync(reportId);
            return Content(_htmlReportBuilderService.Build(ReportTemplates.IndividualReport, model));
        }

        [HttpGet]
        public ActionResult PreviewOld(int reportId)
        {
            var reportService = new UserManagement.Services.ReportService(new Repositories.Context.ApplicationDbContext());
            return Content(reportService.GenerateHTMLReport(reportId));
        }

        [HttpGet]
        public ActionResult PreviewPdf(int reportId)
        {
            return new ActionAsPdf(nameof(Preview), new { reportId = reportId});
        }

        [HttpGet]
        public async Task<ActionResult> GetLatex(int reportId)
        {
            var model = await _reportTemplateService.BuildAsync(reportId);
            var htmlReport = _htmlReportBuilderService.Build(ReportTemplates.IndividualReport, model);
            var texReport = _texReportBuilderService.Build(htmlReport);
            return File(Encoding.GetEncoding(866).GetBytes(texReport), "application/x-latex", "report.tex");
        }
    }
}