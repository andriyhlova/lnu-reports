using AutoMapper;
using PagedList;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using SRS.Web.Models.DissertationDefense;
using SRS.Web.Models.Shared;
using SRS.Web.Models.ThemeOfScientificWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class DissertationDefenseController : Controller
    {
        private readonly IBaseCrudService<DissertationDefenseModel> _dissertationDefenseCrudService;
        private readonly IDissertationDefenseService _dissertationDefenseService;
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<SpecializationModel> _specializationService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public DissertationDefenseController(
            IBaseCrudService<DissertationDefenseModel> dissertationDefenseCrudService,
            IDissertationDefenseService dissertationDefenseService,
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<SpecializationModel> specializationService,
            IExportService exportService,
            IMapper mapper)
        {
            _dissertationDefenseCrudService = dissertationDefenseCrudService;
            _dissertationDefenseService = dissertationDefenseService;
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _specializationService = specializationService;
            _exportService = exportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(DissertationDefenseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<DissertationDefenseFilterModel>(filterViewModel);
            var dissertationDefense = await _dissertationDefenseService.GetAsync(filterModel);
            var total = await _dissertationDefenseService.CountAsync(filterModel);

            await FillAvailableDepartments();

            var viewModel = new ItemsViewModel<DissertationDefenseFilterViewModel, DissertationDefenseModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<DissertationDefenseModel>(dissertationDefense, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await FillAvailableSpecializations();
            return View(new DissertationDefenseModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DissertationDefenseModel dissertationDefense)
        {
            if (ModelState.IsValid)
            {
                await _dissertationDefenseCrudService.AddAsync(dissertationDefense);
                return RedirectToAction(nameof(Index), new { IsActive = true });
            }

            await FillAvailableSpecializations();
            return View(dissertationDefense);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(DissertationDefenseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<DissertationDefenseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var defenses = await _dissertationDefenseService.GetAsync(filterModel);
            var csvModel = new CsvModel<DisertationDefenseCsvModel>
            {
                Data = _mapper.Map<IList<DisertationDefenseCsvModel>>(defenses)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "disertationDefense.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(DissertationDefenseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<DissertationDefenseFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var defenses = await _dissertationDefenseService.GetAsync(filterModel);
            var csvModel = new CsvModel<DisertationDefenseCsvModel>
            {
                Data = _mapper.Map<IList<DisertationDefenseCsvModel>>(defenses)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "disertationDefense.xlsx");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            await FillAvailableSpecializations();
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DissertationDefenseModel dissertationDefense)
        {
            if (ModelState.IsValid)
            {
                await _dissertationDefenseCrudService.UpdateAsync(dissertationDefense);
                return RedirectToAction(nameof(Index), new { IsActive = true });
            }

            await FillAvailableSpecializations();
            return View(dissertationDefense);
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
            await _dissertationDefenseCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var dissertationDefense = await _dissertationDefenseCrudService.GetAsync(id);
            if (dissertationDefense == null)
            {
                return HttpNotFound();
            }

            return View(dissertationDefense);
        }

        private async Task FillAvailableDepartments()
        {
            ViewBag.AllCathedras = await _cathedraService.GetByFacultyAsync(null);
            ViewBag.AllFaculties = await _facultyService.GetAllAsync();
        }

        private async Task FillAvailableSpecializations()
        {
            if (User.IsInRole(RoleNames.Superadmin) || User.IsInRole(RoleNames.RectorateAdmin))
            {
                ViewBag.AllSpecializations = await _specializationService.GetAllAsync();
            }
        }
    }
}