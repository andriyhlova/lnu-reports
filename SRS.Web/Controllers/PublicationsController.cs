using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Repositories.Context;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Publications;
using SRS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UserManagement.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<PublicationModel> _publicationCrudService;
        private readonly IPublicationService _publicationService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IUserService<UserInitialsModel> _userWithInitialService;
        private readonly IMapper _mapper;

        public PublicationsController(
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<PublicationModel> publicationCrudService,
            IPublicationService publicationService,
            IUserService<UserAccountModel> userService,
            IUserService<UserInitialsModel> userWithInitialService,
            IMapper mapper)
        {
            _cathedraService = cathedraService;
            _facultyService = facultyService;
            _publicationCrudService = publicationCrudService;
            _publicationService = publicationService;
            _userService = userService;
            _userWithInitialService = userWithInitialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(PublicationFilterViewModel filterViewModel)
        {
            var user = await _userService.GetByIdAsync(User.Identity.GetUserId());
            var filterModel = _mapper.Map<PublicationFilterModel>(filterViewModel);
            var publications = await _publicationService.GetPublicationsForUserAsync(user, filterModel);
            var total = await _publicationService.CountPublicationsForUserAsync(user, filterModel);

            await FillAvailableDepartments(user.FacultyId);

            var viewModel = new ItemsViewModel<PublicationFilterViewModel, BasePublicationModel>
            {
                FilterModel = filterViewModel,
                Items = new StaticPagedList<BasePublicationModel>(publications, filterViewModel.Page.Value, PaginationValues.PageSize, total)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var publication = await _publicationCrudService.GetAsync(id);
            if (publication == null)
            {
                return HttpNotFound();
            }

            return View(publication);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            FillRelatedEntities();
            var currentUser = await _userWithInitialService.GetByIdAsync(User.Identity.GetUserId());
            var model = new PublicationEditViewModel
            {
                Users = new List<UserInitialsModel> { currentUser },
                Year = DateTime.Now.Year
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PublicationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _publicationCrudService.AddAsync(_mapper.Map<PublicationModel>(model));
                return RedirectToAction(nameof(Index));
            }

            FillRelatedEntities();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var publication = await _publicationCrudService.GetAsync(id);
            if (publication == null)
            {
                return HttpNotFound();
            }

            FillRelatedEntities();
            return View(_mapper.Map<PublicationEditViewModel>(publication));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PublicationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _publicationCrudService.UpdateAsync(_mapper.Map<PublicationModel>(model));
                return RedirectToAction(nameof(Index));
            }

            FillRelatedEntities();
            return View(model);
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
            await _publicationCrudService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task FillAvailableDepartments(int? facultyId)
        {
            if (User.IsInRole(RoleNames.Superadmin) || User.IsInRole(RoleNames.RectorateAdmin))
            {
                ViewBag.AllCathedras = await _cathedraService.GetByFacultyAsync(null);
                ViewBag.AllFaculties = await _facultyService.GetAllAsync();
            }
            else if (User.IsInRole(RoleNames.DeaneryAdmin))
            {
                ViewBag.AllCathedras = await _cathedraService.GetByFacultyAsync(facultyId);
            }
        }

        private void FillRelatedEntities()
        {
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Where(x => x != nameof(PublicationType.Стаття))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' ').Replace(" які", ", які"), Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
        }
    }
}
