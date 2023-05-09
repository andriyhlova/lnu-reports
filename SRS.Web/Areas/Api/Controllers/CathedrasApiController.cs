using SRS.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class CathedrasApiController : Controller
    {
        private readonly ICathedraService _cathedraService;

        public CathedrasApiController(ICathedraService cathedrasService)
        {
            _cathedraService = cathedrasService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var cathedra = await _cathedraService.GetByFacultyAsync(null);
            return Json(cathedra, JsonRequestBehavior.AllowGet);
        }
    }
}