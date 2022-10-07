using System.Threading.Tasks;
using System.Web.Mvc;
using SRS.Domain.Enums;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class JournalsApiController : Controller
    {
        private readonly IJournalService _journalService;

        public JournalsApiController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpGet]
        public async Task<ActionResult> GetByPublicationType(PublicationType? publicationType)
        {
            var users = await _journalService.GetAllAsync(new JournalFilterModel { PublicationType = publicationType, OrderBy = (int)JournalOrderType.Name });
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}
