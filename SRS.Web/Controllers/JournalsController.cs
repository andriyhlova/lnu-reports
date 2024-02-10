using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.JournalModels;
using SRS.Web.Models.Journals;
using SRS.Web.Models.Shared;
using SRS.Web.Models.ThemeOfScientificWorks;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class JournalsController : Controller
    {
        private readonly IBaseCrudService<JournalModel> _journalCrudService;
        private readonly IJournalService _journalService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public JournalsController(
            IBaseCrudService<JournalModel> journalCrudService,
            IJournalService journalService,
            IExportService exportService,
            IMapper mapper)
        {
            _journalCrudService = journalCrudService;
            _journalService = journalService;
            _exportService = exportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(JournalFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<JournalFilterModel>(filterViewModel);
            var journals = await _journalService.GetAllAsync(filterModel);
            var total = await _journalService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<JournalFilterViewModel, JournalModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<JournalModel>(journals, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var journal = await _journalCrudService.GetAsync(id);
            if (journal == null)
            {
                return HttpNotFound();
            }

            return View(journal);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new JournalModel() { JournalTypes = new List<JournalTypeModel>() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JournalModel journal)
        {
            if (ModelState.IsValid)
            {
                await _journalCrudService.AddAsync(journal);
                return RedirectToAction(nameof(Index));
            }

            return View(journal);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(JournalFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<JournalFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var journals = await _journalService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<JournalCsvModel>
            {
                Data = _mapper.Map<IList<JournalCsvModel>>(journals)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "journal.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(JournalFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<JournalFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var journals = await _journalService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<JournalCsvModel>
            {
                Data = _mapper.Map<IList<JournalCsvModel>>(journals)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "journal.xlsx");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JournalModel journal)
        {
            if (ModelState.IsValid)
            {
                await _journalCrudService.UpdateAsync(journal);
                return RedirectToAction(nameof(Index));
            }

            return View(journal);
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
            await _journalCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
