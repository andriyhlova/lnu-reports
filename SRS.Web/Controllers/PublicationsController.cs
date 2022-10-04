using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.Publications;
using SRS.Web.Models.Shared;

namespace SRS.Web.Controllers
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
            var publications = await _publicationService.GetForUserAsync(user, filterModel);
            var total = await _publicationService.CountForUserAsync(user, filterModel);

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

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View(publication);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var currentUser = await _userWithInitialService.GetByIdAsync(User.Identity.GetUserId());
            var model = new PublicationEditViewModel
            {
                Users = new List<UserInitialsModel> { currentUser },
                Year = DateTime.Now.Year
            };

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PublicationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _publicationCrudService.AddAsync(_mapper.Map<PublicationModel>(model));
                return RedirectToIndex();
            }

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
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

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View(_mapper.Map<PublicationEditViewModel>(publication));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PublicationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _publicationCrudService.UpdateAsync(_mapper.Map<PublicationModel>(model));
                return RedirectToIndex();
            }

            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            return await Details(id);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _publicationCrudService.DeleteAsync(id);
            return RedirectToIndex();
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

        private RedirectResult RedirectToIndex()
        {
            var returnUrl = Request.QueryString["returnUrl"];
            return Redirect(Url.Action(nameof(Index)) + (!string.IsNullOrWhiteSpace(returnUrl) ? "?" + returnUrl : string.Empty));
        }
    }
}
