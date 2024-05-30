using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Rotativa;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.Constants;
using SRS.Services.Models.ReportGenerationModels.DepartmentReport;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    public class CathedraReportGenerationController : Controller
    {
        private readonly ICathedraReportTemplateService _cathedraReportTemplateService;
        private readonly IFacultyReportTemplateService _facultyReportTemplateService;
        private readonly IHtmlReportBuilderService<DepartmentReportTemplateModel> _htmlCathedraReportBuilderService;
        private readonly ITexReportBuilderService _texReportBuilderService;
        private readonly IWordReportBuilderService _wordCathedraReportBuilderService;

        public CathedraReportGenerationController(
            ICathedraReportTemplateService cathedraReportTemplateService,
            IFacultyReportTemplateService facultyReportTemplateService,
            IHtmlReportBuilderService<DepartmentReportTemplateModel> htmlCathedraReportBuilderService,
            ITexReportBuilderService texReportBuilderService,
            IWordReportBuilderService wordCathedraReportBuilderService)
        {
            _cathedraReportTemplateService = cathedraReportTemplateService;
            _facultyReportTemplateService = facultyReportTemplateService;
            _htmlCathedraReportBuilderService = htmlCathedraReportBuilderService;
            _texReportBuilderService = texReportBuilderService;
            _wordCathedraReportBuilderService = wordCathedraReportBuilderService;
        }

        [HttpGet]
        public async Task<ActionResult> Preview(int reportId, string department)
        {
            if (department == Departments.Cathedra)
            {
                var model = await _cathedraReportTemplateService.BuildAsync(reportId);

                return Content(_htmlCathedraReportBuilderService.Build(ReportTemplates.CathedraReport, model));
            }
            else if (department == Departments.Faculty)
            {
                var model = await _facultyReportTemplateService.BuildAsync(reportId);

                return Content(_htmlCathedraReportBuilderService.Build(ReportTemplates.CathedraReport, model));
            }

            return View();
        }

        [HttpGet]
        public ActionResult PreviewPdf(int reportId)
        {
            var cookies = Request.Cookies.AllKeys.ToDictionary(k => k, k => Request.Cookies[k].Value);
            return new ActionAsPdf(nameof(Preview), new { reportId })
            {
                FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName,
                Cookies = cookies
            };
        }

        [HttpGet]
        public async Task<ActionResult> GetLatex(int reportId)
        {
            var model = await _cathedraReportTemplateService.BuildAsync(reportId);
            var htmlReport = _htmlCathedraReportBuilderService.Build(ReportTemplates.CathedraReport, model);
            var texReport = _texReportBuilderService.Build(htmlReport);
            return File(Encoding.GetEncoding(866).GetBytes(texReport), "application/x-latex", "report.tex");
        }

        [HttpGet]
        public async Task<ActionResult> GetWord(int reportId)
        {
            var model = await _cathedraReportTemplateService.BuildAsync(reportId);
            var htmlReport = _htmlCathedraReportBuilderService.Build(ReportTemplates.CathedraReport, model);
            var wordReport = _wordCathedraReportBuilderService.Build(htmlReport);
            return File(
                    fileContents: wordReport,
                    contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    fileDownloadName: $"report.docx");
        }
    }
}