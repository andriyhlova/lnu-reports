using Rotativa;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.Constants;
using SRS.Services.Models.ReportGenerationModels.CathedraReport;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    public class CathedraReportGenerationController : Controller
    {
        private readonly ICathedraReportTemplateService _cathedraReportTemplateService;
        private readonly IHtmlReportBuilderService<CathedraReportTemplateModel> _htmlCathedraReportBuilderService;
        private readonly ITexReportBuilderService _texReportBuilderService;

        public CathedraReportGenerationController(
            ICathedraReportTemplateService cathedraReportTemplateService,
            IHtmlReportBuilderService<CathedraReportTemplateModel> htmlCathedraReportBuilderService,
            ITexReportBuilderService texReportBuilderService)
        {
            _cathedraReportTemplateService = cathedraReportTemplateService;
            _htmlCathedraReportBuilderService = htmlCathedraReportBuilderService;
            _texReportBuilderService = texReportBuilderService;
        }

        [HttpGet]
        public async Task<ActionResult> Preview(int reportId)
        {
            var model = await _cathedraReportTemplateService.BuildAsync(reportId);
            return Content(_htmlCathedraReportBuilderService.Build(ReportTemplates.CathedraReport, model));
        }

        [HttpGet]
        public ActionResult PreviewOld(int reportId)
        {
            var reportService = new UserManagement.Services.CathedraReportService(new Repositories.Context.ApplicationDbContext());
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
            var model = await _cathedraReportTemplateService.BuildAsync(reportId);
            var htmlReport = _htmlCathedraReportBuilderService.Build(ReportTemplates.CathedraReport, model);
            var texReport = _texReportBuilderService.Build(htmlReport);
            return File(Encoding.GetEncoding(866).GetBytes(texReport), "application/x-latex", "report.tex");
        }
    }
}