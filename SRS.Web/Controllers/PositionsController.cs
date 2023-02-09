﻿using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class PositionsController : Controller
    {
        private readonly IBaseCrudService<PositionModel> _positionsCrudService;
        private readonly IPositionService _positionsService;
        private readonly IMapper _mapper;

        public PositionsController(
            IBaseCrudService<PositionModel> positionsCrudService,
            IPositionService positionsService,
            IMapper mapper)
        {
            _positionsCrudService = positionsCrudService;
            _positionsService = positionsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(BaseFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(filterViewModel);
            var positions = await _positionsService.GetAllAsync(filterModel);
            var total = await _positionsService.CountAsync(filterModel);
            var viewModel = new ItemsViewModel<BaseFilterViewModel, PositionModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<PositionModel>(positions, filterViewModel.Page.Value, PaginationValues.PageSize, total)
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
        public async Task<ActionResult> Create(PositionModel positions)
        {
            if (ModelState.IsValid)
            {
                await _positionsCrudService.AddAsync(positions);
                return RedirectToAction(nameof(Index));
            }

            return View(positions);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PositionModel positions)
        {
            if (ModelState.IsValid)
            {
                await _positionsCrudService.UpdateAsync(positions);
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
            await _positionsCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ActionResult> Details(int id)
        {
            var positions = await _positionsCrudService.GetAsync(id);
            if (positions == null)
            {
                return HttpNotFound();
            }

            return View(positions);
        }
    }
}