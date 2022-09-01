using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class HonoraryTitlesController : Controller
    {
        private readonly IHonoraryTitleService _honoraryTitlesService;

        public HonoraryTitlesController(IHonoraryTitleService honoraryTitlesService)
        {
            _honoraryTitlesService = honoraryTitlesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var degrees = await _honoraryTitlesService.GetAllAsync(new BaseFilterModel());
            return Json(degrees, JsonRequestBehavior.AllowGet);
        }
    }
}
