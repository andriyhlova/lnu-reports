using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models.JournalModels;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class JournalTypesController : Controller
    {
        private readonly IBaseCrudService<JournalTypeModel> _journalTypeService;

        public JournalTypesController(IBaseCrudService<JournalTypeModel> journalTypeService)
        {
            _journalTypeService = journalTypeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _journalTypeService.GetAllAsync();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
