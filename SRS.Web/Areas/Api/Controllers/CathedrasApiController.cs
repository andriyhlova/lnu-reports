using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;

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
            var cathedra = await _cathedraService.GetAllAsync(new BaseFilterModel());
            return Json(cathedra, JsonRequestBehavior.AllowGet);
        }
    }
}