using AutoMapper;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class HonoraryTitlesController : Controller
    {
        private readonly IBaseCrudService<HonoraryTitleModel> _honoraryTitlesCrudService;
        private readonly IHonoraryTitleService _honoraryTitlesService;
        private readonly IMapper _mapper;

        public HonoraryTitlesController(
            IBaseCrudService<HonoraryTitleModel> honoraryTitlesCrudService,
            IHonoraryTitleService honoraryTitlesService,
            IMapper mapper)
        {
            _honoraryTitlesCrudService = honoraryTitlesCrudService;
            _honoraryTitlesService = honoraryTitlesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var honoraryTitles = await _honoraryTitlesService.GetAllAsync(filterModel);
            var total = await _honoraryTitlesService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, HonoraryTitleModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<HonoraryTitleModel>(honoraryTitles, filterViewModel.Page.Value, PaginationValues.PageSize, total)
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
        public async Task<ActionResult> Create(HonoraryTitleModel honoraryTitles)
        {
            if (ModelState.IsValid)
            {
                await _honoraryTitlesCrudService.AddAsync(honoraryTitles);
                return RedirectToAction(nameof(Index));
            }

            return View(honoraryTitles);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HonoraryTitleModel honoraryTitles)
        {
            if (ModelState.IsValid)
            {
                await _honoraryTitlesCrudService.UpdateAsync(honoraryTitles);
                return RedirectToAction(nameof(Index));
            }

            return View(honoraryTitles);
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
            await _honoraryTitlesCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ActionResult> Details(int id)
        {
            var honoraryTitles = await _honoraryTitlesCrudService.GetAsync(id);
            if (honoraryTitles == null)
            {
                return HttpNotFound();
            }

            return View(honoraryTitles);
        }
    }
}
