using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class DegreesController : Controller
    {
        private readonly IBaseCrudService<DegreeModel> _degreesCrudService;
        private readonly IDegreeService _degreesService;
        private readonly IMapper _mapper;

        public DegreesController(
            IBaseCrudService<DegreeModel> degreesCrudService,
            IDegreeService degreesService,
            IMapper mapper)
        {
            _degreesCrudService = degreesCrudService;
            _degreesService = degreesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var degrees = await _degreesService.GetAllAsync(filterModel);
            var total = await _degreesService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, DegreeModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<DegreeModel>(degrees, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DegreeModel degrees)
        {
            if (ModelState.IsValid)
            {
                await _degreesCrudService.AddAsync(degrees);
                return RedirectToAction(nameof(Index));
            }

            return View(degrees);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DegreeModel degrees)
        {
            if (ModelState.IsValid)
            {
                await _degreesCrudService.UpdateAsync(degrees);
                return RedirectToAction(nameof(Index));
            }

            return View(degrees);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _degreesCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ActionResult> Details(int id)
        {
            var degrees = await _degreesCrudService.GetAsync(id);
            if (degrees == null)
            {
                return HttpNotFound();
            }

            return View(degrees);
        }
    }
}
