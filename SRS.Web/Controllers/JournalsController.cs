using AutoMapper;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class JournalsController : Controller
    {
        private readonly IBaseCrudService<JournalModel> _journalCrudService;
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalsController(
            IBaseCrudService<JournalModel> journalCrudService,
            IJournalService journalService,
            IMapper mapper)
        {
            _journalCrudService = journalCrudService;
            _journalService = journalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var journals = await _journalService.GetAllAsync(filterModel);
            var total = await _journalService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, JournalModel>
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
            FillQuartiles();
            return View();
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

            FillQuartiles();
            return View(journal);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            FillQuartiles();
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

            FillQuartiles();
            return View(journal);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return await Details(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _journalCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private void FillQuartiles()
        {
            ViewBag.AllQuartiles = Enum.GetNames(typeof(Quartile))
                .Select(x => new SelectListItem { Text = x, Value = x })
                .ToList();
        }
    }
}
