using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class DegreesApiController : Controller
    {
        private readonly IDegreeService _degreesService;

        public DegreesApiController(IDegreeService degreesService)
        {
            _degreesService = degreesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var degrees = await _degreesService.GetAllAsync(new BaseFilterModel());
            return Json(degrees, JsonRequestBehavior.AllowGet);
        }
    }
}
