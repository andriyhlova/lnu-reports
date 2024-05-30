using AutoMapper;
using PagedList;
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
    public class SpecializationsController : Controller
    {
        private readonly IBaseCrudService<SpecializationModel> _specializationsCrudService;
        private readonly ISpecializationService _specializationsService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public SpecializationsController(
            IBaseCrudService<SpecializationModel> specializationsCrudService,
            ISpecializationService specializationsService,
            IExportService exportService,
            IMapper mapper)
        {
            _specializationsCrudService = specializationsCrudService;
            _specializationsService = specializationsService;
            _exportService = exportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var specializations = await _specializationsService.GetAllAsync(filterModel);
            var total = await _specializationsService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, SpecializationModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<SpecializationModel>(specializations, filterViewModel.Page.Value, PaginationValues.PageSize, total)
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
        public async Task<ActionResult> Create(SpecializationModel positions)
        {
            if (ModelState.IsValid)
            {
                await _specializationsCrudService.AddAsync(positions);
                return RedirectToAction(nameof(Index));
            }

            return View(positions);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var specializations = await _specializationsService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<SpecializationCsvModel>
            {
                Data = _mapper.Map<IList<SpecializationCsvModel>>(specializations)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "specialization.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(FacultyFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var specializations = await _specializationsService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<SpecializationCsvModel>
            {
                Data = _mapper.Map<IList<SpecializationCsvModel>>(specializations)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "specialization.xlsx");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SpecializationModel positions)
        {
            if (ModelState.IsValid)
            {
                await _specializationsCrudService.UpdateAsync(positions);
                return RedirectToAction(nameof(Index));
            }

            return View(positions);
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
            await _specializationsCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ActionResult> Details(int id)
        {
            var positions = await _specializationsCrudService.GetAsync(id);
            if (positions == null)
            {
                return HttpNotFound();
            }

            return View(positions);
        }
    }
}