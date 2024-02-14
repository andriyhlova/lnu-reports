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
    public class FacultiesController : Controller
    {
        private readonly IBaseCrudService<FacultyModel> _facultiesCrudService;
        private readonly IFacultyService _facultiesService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public FacultiesController(
            IBaseCrudService<FacultyModel> facultiesCrudService,
            IFacultyService facultiesService,
            IExportService exportService,
            IMapper mapper)
        {
            _facultiesCrudService = facultiesCrudService;
            _facultiesService = facultiesService;
            _exportService = exportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var faculties = await _facultiesService.GetAllAsync(filterModel);
            var total = await _facultiesService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, FacultyModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<FacultyModel>(faculties, filterViewModel.Page.Value, PaginationValues.PageSize, total)
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
        public async Task<ActionResult> Create(FacultyModel faculties)
        {
            if (ModelState.IsValid)
            {
                await _facultiesCrudService.AddAsync(faculties);
                return RedirectToAction(nameof(Index));
            }

            return View(faculties);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var faculties = await _facultiesService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<FacultyCsvModel>
            {
                Data = _mapper.Map<IList<FacultyCsvModel>>(faculties)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "faculty.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var faculties = await _facultiesService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<FacultyCsvModel>
            {
                Data = _mapper.Map<IList<FacultyCsvModel>>(faculties)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "faculty.xlsx");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FacultyModel faculties)
        {
            if (ModelState.IsValid)
            {
                await _facultiesCrudService.UpdateAsync(faculties);
                return RedirectToAction(nameof(Index));
            }

            return View(faculties);
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
            await _facultiesCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ActionResult> Details(int id)
        {
            var degrees = await _facultiesCrudService.GetAsync(id);
            if (degrees == null)
            {
                return HttpNotFound();
            }

            return View(degrees);
        }
    }
}
