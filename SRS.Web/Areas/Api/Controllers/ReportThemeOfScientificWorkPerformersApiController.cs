using SRS.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Areas.Api.Controllers
{
    public class ReportThemeOfScientificWorkPerformersApiController : Controller
    {
        private readonly IReportService _reportService;

        public ReportThemeOfScientificWorkPerformersApiController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult> Search(int reportId)
        {
            var reportThemeOfScientificWork = await _reportService.GetAsyncReportThemeOfScientificWork(reportId);
            return Json(reportThemeOfScientificWork, JsonRequestBehavior.AllowGet);
        }
    }
}