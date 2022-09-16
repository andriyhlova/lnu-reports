using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class ThemeOfScientificWorksApiController : Controller
    {
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;

        public ThemeOfScientificWorksApiController(IThemeOfScientificWorkService themeOfScientificWorkService)
        {
            _themeOfScientificWorkService = themeOfScientificWorkService;
        }

        [HttpGet]
        public async Task<ActionResult> Search(string search, Financial[] financials)
        {
            var users = await _themeOfScientificWorkService.GetActiveAsync(new ThemeOfScientificWorkFilterModel { Search = search }, financials);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
