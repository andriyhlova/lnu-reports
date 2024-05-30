using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Areas.Api.Controllers
{
    public class DissertationDefenseApiController : Controller
    {
        private readonly IDissertationDefenseService _dissertationDefenseService;

        public DissertationDefenseApiController(IDissertationDefenseService dissertationDefenseService)
        {
            _dissertationDefenseService = dissertationDefenseService;
        }

        [HttpGet]
        public async Task<ActionResult> Search(string search)
        {
            var defenses = await _dissertationDefenseService.GetAsync(new DissertationDefenseFilterModel { Search = search, YearOfGraduatingFrom = DateTime.Now.Year - 1 });
            return Json(defenses, JsonRequestBehavior.AllowGet);
        }
    }
}