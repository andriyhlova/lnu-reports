using AutoMapper;
using Org.BouncyCastle.Asn1.Pkcs;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class CathedrasController : Controller
    {
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<CathedraModel> _cathedraCrudService;
        private readonly ICathedraService _cathedraService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public CathedrasController(
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<CathedraModel> cathedraCrudService,
            ICathedraService cathedraService,
            IExportService exportService,
            IMapper mapper)
        {
            _cathedraCrudService = cathedraCrudService;
            _facultyService = facultyService;
            _cathedraService = cathedraService;
            _exportService = exportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(FacultyFilterViewModel filterViewModel)
        {
            await FillAvailableFaculties();

            var filterModel = _mapper.Map<FacultyFilterModel>(filterViewModel);
            var cathedras = await _cathedraService.GetAllAsync(filterModel);
            var total = await _cathedraService.CountAsync(filterModel);

            var viewModel = new ItemsViewModel<FacultyFilterViewModel, CathedraModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<CathedraModel>(cathedras, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var cathedra = await _cathedraCrudService.GetAsync(id);
            if (cathedra == null)
            {
                return HttpNotFound();
            }

            return View(cathedra);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            await FillAvailableFaculties();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CathedraModel cathedra)
        {
            if (ModelState.IsValid)
            {
                await _cathedraCrudService.AddAsync(cathedra);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            await FillAvailableFaculties();
            return View(cathedra);
        }

        [HttpGet]
        public async Task<ActionResult> ExportToCsv(FacultyFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<FacultyFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var cathedras = await _cathedraService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<CathedraCsvModel>
            {
                Data = _mapper.Map<IList<CathedraCsvModel>>(cathedras)
            };

            byte[] fileBytes = _exportService.WriteCsv(csvModel);
            return File(fileBytes, "text/csv", "cathedra.csv");
        }

        [HttpGet]
        public async Task<ActionResult> ExportToExcel(FacultyFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<FacultyFilterModel>(filterViewModel);

            filterModel.Take = null;
            filterModel.Skip = null;
            var cathedras = await _cathedraService.GetAllAsync(filterModel);
            var csvModel = new CsvModel<CathedraCsvModel>
            {
                Data = _mapper.Map<IList<CathedraCsvModel>>(cathedras)
            };

            byte[] fileBytes = _exportService.WriteExcel(csvModel);
            return File(fileBytes, "text/xcls", "cathedra.xlsx");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            await FillAvailableFaculties();
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CathedraModel cathedra)
        {
            if (ModelState.IsValid)
            {
                await _cathedraCrudService.UpdateAsync(cathedra);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            await FillAvailableFaculties();
            return View(cathedra);
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
            await _cathedraCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task FillAvailableFaculties()
        {
            if (User.IsInRole(RoleNames.Superadmin) || User.IsInRole(RoleNames.RectorateAdmin))
            {
                ViewBag.AllFaculties = await _facultyService.GetAllAsync();
            }
        }
    }
}
