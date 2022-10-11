﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using SRS.Web.Models.Shared;
using SRS.Web.Models.ThemeOfScientificWorks;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату")]
    public class ThemeOfScientificWorksController : Controller
    {
        private readonly IBaseCrudService<ThemeOfScientificWorkModel> _themeOfScientificWorkCrudService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IMapper _mapper;

        public ThemeOfScientificWorksController(
            IBaseCrudService<ThemeOfScientificWorkModel> themeOfScientificWorkCrudService,
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IMapper mapper)
        {
            _themeOfScientificWorkCrudService = themeOfScientificWorkCrudService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(ThemeOfScientificWorkFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<ThemeOfScientificWorkFilterModel>(filterViewModel);
            var scientificThemes = await _themeOfScientificWorkService.GetAsync(filterModel);
            var total = await _themeOfScientificWorkService.CountAsync(filterModel);

            var viewModel = new ItemsViewModel<ThemeOfScientificWorkFilterViewModel, BaseThemeOfScientificWorkWithFinancialsModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BaseThemeOfScientificWorkWithFinancialsModel>(scientificThemes, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var themeOfScientificWork = await _themeOfScientificWorkCrudService.GetAsync(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }

            return View(themeOfScientificWork);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ThemeOfScientificWorkModel { ThemeOfScientificWorkFinancials = new List<ThemeOfScientificWorkFinancialModel>() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkCrudService.AddAsync(themeOfScientificWork);
                return RedirectToAction(nameof(Index), new { IsActive = true });
            }

            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThemeOfScientificWorkModel themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorkCrudService.UpdateAsync(themeOfScientificWork);
                return RedirectToAction(nameof(Index), new { IsActive = true });
            }

            return View(themeOfScientificWork);
        }

        [HttpGet]
        public async Task<ActionResult> ToggleActivation(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ActionName("ToggleActivation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ToggleActivationConfirmed(int id)
        {
            await _themeOfScientificWorkService.ToggleActivationAsync(id);
            return RedirectToAction(nameof(Index), new { IsActive = true });
        }
    }
}
