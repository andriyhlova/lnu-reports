using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class JournalsApiController : Controller
    {
        private readonly IBaseCrudService<JournalModel> _journalService;

        public JournalsApiController(IBaseCrudService<JournalModel> journalService)
        {
            _journalService = journalService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _journalService.GetAllAsync();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
