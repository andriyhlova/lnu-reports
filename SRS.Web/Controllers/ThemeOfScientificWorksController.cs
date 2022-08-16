﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Domain.Enums;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Extensions;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Керівник кафедри, Адміністрація деканату")]
    public class ThemeOfScientificWorksController : Controller
    {
        private readonly IBaseCrudService<ThemeOfScientificWorkModel> _themeOfScientificWorkCrudService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IMapper _mapper;

        public ThemeOfScientificWorksController(
            IBaseCrudService<ThemeOfScientificWorkModel> themeOfScientificWorkCrudService,
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IUserService<UserAccountModel> userService,
            IMapper mapper)
        {
            _themeOfScientificWorkCrudService = themeOfScientificWorkCrudService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(DepartmentFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<DepartmentFilterModel>(filterViewModel);
            var scientifthemes = await _themeOfScientificWorkService.GetForUserAsync(user, filterModel);
            var total = await _themeOfScientificWorkService.CountForUserAsync(user, filterModel);

            var viewModel = new ItemsViewModel<DepartmentFilterViewModel, ThemeOfScientificWorkModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<ThemeOfScientificWorkModel>(scientifthemes, filterViewModel.Page.Value, PaginationValues.PageSize, total)
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
#pragma warning disable S2486 // Generic exceptions should not be ignored
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable RCS1075 // Avoid empty catch clause that catches System.Exception.
                try
                {
                    await _themeOfScientificWorkCrudService.AddAsync(themeOfScientificWork);
                }
                catch (Exception exc)
#pragma warning disable S108 // Nested blocks of code should not be left empty
                {
                }
#pragma warning restore S108 // Nested blocks of code should not be left empty
#pragma warning restore RCS1075 // Avoid empty catch clause that catches System.Exception.
#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore S2486 // Generic exceptions should not be ignored

                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }

            return View(themeOfScientificWork);
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
            await _themeOfScientificWorkCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
