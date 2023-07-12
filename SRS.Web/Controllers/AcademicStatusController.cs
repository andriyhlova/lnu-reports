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
    public class AcademicStatusController : Controller
    {
        private readonly IBaseCrudService<AcademicStatusModel> _academicStatusCrudService;
        private readonly IAcademicStatusService _academicStatusService;
        private readonly IMapper _mapper;

        public AcademicStatusController(
            IBaseCrudService<AcademicStatusModel> academicStatusCrudService,
            IAcademicStatusService academicStatusService,
            IMapper mapper)
        {
            _academicStatusCrudService = academicStatusCrudService;
            _academicStatusService = academicStatusService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var academicStatus = await _academicStatusService.GetAllAsync(filterModel);
            var total = await _academicStatusService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, AcademicStatusModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<AcademicStatusModel>(academicStatus, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AcademicStatusModel academicStatus)
        {
            if (ModelState.IsValid)
            {
                await _academicStatusCrudService.UpdateAsync(academicStatus);
                return RedirectToAction(nameof(Index));
            }

            return View(academicStatus);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcademicStatusModel academicStatus)
        {
            if (ModelState.IsValid)
            {
                await _academicStatusCrudService.AddAsync(academicStatus);
                return RedirectToAction(nameof(Index));
            }

            return View(academicStatus);
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
            await _academicStatusCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ActionResult> Details(int id)
        {
            var academicStatus = await _academicStatusCrudService.GetAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }

            return View(academicStatus);
        }
    }
}