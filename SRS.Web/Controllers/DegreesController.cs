using AutoMapper;
using PagedList;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class DegreesController : Controller
    {
        private readonly IBaseCrudService<DegreeModel> _degreesCrudService;
        private readonly IDegreeService _degreesService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public DegreesController(
            IBaseCrudService<DegreeModel> degreesCrudService,
            IDegreeService degreesService,
            IExportService exportService,
            IMapper mapper)
        {
            _degreesCrudService = degreesCrudService;
            _degreesService = degreesService;
            _exportService = exportService;
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
        public async Task<ActionResult> ExportToCsv(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var degrees = await _degreesService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<DegreeCsvModel>
            {
                Data = _mapper.Map<IList<DegreeCsvModel>>(degrees)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "degrees.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var degrees = await _degreesService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<DegreeCsvModel>
            {
                Data = _mapper.Map<IList<DegreeCsvModel>>(degrees)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "degrees.xlsx");
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
