using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class AcademicStatusesController : Controller
    {
        private readonly IAcademicStatusService _academicStatusesService;

        public AcademicStatusesController(IAcademicStatusService academicStatusesService)
        {
            _academicStatusesService = academicStatusesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var academicStatuses = await _academicStatusesService.GetAllAsync(new BaseFilterModel());
            return Json(academicStatuses, JsonRequestBehavior.AllowGet);
        }
    }
}
