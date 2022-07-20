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
        private ApplicationDbContext db = new ApplicationDbContext();

        private readonly IBaseCrudService<CathedraModel> _cathedraCrudService;
        private readonly ICathedraService _cathedraService;
        private readonly IBaseCrudService<FacultyModel> _facultyService;
        private readonly IBaseCrudService<PublicationModel> _publicationCrudService;
        private readonly IPublicationService _publicationService;
        private readonly IUserService<UserAccountModel> _userService;
        private readonly IUserService<UserInitialsModel> _userWithInitialService;
        private readonly IMapper _mapper;

        public PublicationsController(
            IBaseCrudService<CathedraModel> cathedraCrudService,
            ICathedraService cathedraService,
            IBaseCrudService<FacultyModel> facultyService,
            IBaseCrudService<PublicationModel> publicationCrudService,
            IPublicationService publicationService,
            IUserService<UserAccountModel> userService,
            IUserService<UserInitialsModel> userWithInitialService,
            IMapper mapper)
        {
            _cathedraCrudService = cathedraCrudService;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "Id,Name,OtherAuthors,PagesFrom,PagesTo,PublicationType,Place," +
            "MainAuthor,IsMainAuthorRegistered,Language,Link,Edition,Magazine,DOI,Tome")] Publication publication, int? year,
            [Bind(Include = "IsMainAuthorRegistered")] bool? mainAuthorFromOthers, [Bind(Include = "authorsOrder")] string[] authorsOrder, [Bind(Include = "PagesFrom")] int pagesFrom = -1,
            [Bind(Include = "PagesTo")] int pagesTo = -1)
        {
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Where(x => x != nameof(PublicationType.Стаття))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = db.Users.Include(x => x.I18nUserInitials).Include(x => x.Roles).ToList();
            var roles = db.Roles.Where(x => x.Name == "Працівник"
            || x.Name == "Керівник кафедри"
            || x.Name == "Адміністрація ректорату"
            || x.Name == "Адміністрація деканату").Select(x => x.Id).ToList();
            ViewBag.AllUsers = users
                .Where(x => x.IsActive && x.Roles.Any(y => roles.Contains(y.RoleId)))
                .Select(x =>
                {
                    var name = x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language);
                    return new SelectListItem
                    {
                        Selected = false,
                        Text = String.Join(" ", name?.LastName,
                                                name?.FirstName,
                                                name?.FathersName),
                        Value = x.Id
                    };
                }).ToList();

            var userToAdd = new List<ApplicationUser>();
            publication.OtherAuthors = publication.OtherAuthors?.Trim();
            var authorsArr = publication.OtherAuthors?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (publication.OtherAuthors != null)
            {
                foreach (var item in authorsArr)
                {
                    var splitted = item.Split(' ');
                    if (splitted.Length == 3)
                    {
                        var firstName = splitted[1];
                        var lastName = splitted[0];
                        var middleName = splitted[2];
                        var user = db.Users.FirstOrDefault(x=>
                        x.I18nUserInitials.Any(y=>y.FirstName == firstName && y.LastName == lastName && y.FathersName == middleName
                        && y.Language == publication.Language));
                        if (user != null)
                        {
                            userToAdd.Add(user);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(publication.Name))
            {
                ModelState.AddModelError(nameof(publication.Name), "Введіть назву публікації");
            }
            if (!year.HasValue)
            {
                ModelState.AddModelError(nameof(year), "Введіть рік публікації");
            }
            if((userToAdd == null || userToAdd.Count == 0) && string.IsNullOrEmpty(publication.OtherAuthors))
            {

                ModelState.AddModelError(nameof(userToAdd), "Додайте авторів");
            }
            if (ModelState.IsValid)
            {
                if (userToAdd != null)
                {
                    var userIds = userToAdd.Select(x => x.Id);
                    var publicationExists = db.Publication
                        .Any(x =>
                        x.Name == publication.Name
                        && x.User.All(y=>userIds.Contains(y.Id))
                        && x.PublicationType == publication.PublicationType
                        );
                    if (publicationExists)
                    {
                        ModelState.AddModelError("", "Така публікація вже існує");
                        return View(publication);
                    }
                    if (userToAdd.Count != 0)
                    {
                        foreach (var current in userToAdd)
                        {
                            publication.User.Add(current);
                            current.Publication.Add(publication);
                        }
                    }
                }
                var authors = string.Empty;
                if (mainAuthorFromOthers.Value)
                {
                    if(!string.IsNullOrEmpty(publication.OtherAuthors))
                    {
                        var values = authorsArr;
                        values = values[0].Split();

                        if (values.Length >= 2)
                        {
                            publication.MainAuthor = values[0] + " " + values[1].FirstOrDefault() + ".";
                            authors += values[1]?.FirstOrDefault() + ". ";
                            if (values.Length == 3)
                            {
                                publication.MainAuthor += " "+values[2].FirstOrDefault()+".";
                                authors += values[2]?.FirstOrDefault() + ". ";
                            }
                            authors += values[0];
                        }
                    }
                }
                if (!string.IsNullOrEmpty(publication.OtherAuthors))
                {
                    var otherAuthors = authorsArr;
                    var start = 0;
                    if (mainAuthorFromOthers.Value)
                        start = 1;
                    for (int i = start; i < otherAuthors.Length; i++)
                    {
                        var author = otherAuthors[i].Split();
                        if(author.Length >= 2)
                        {
                            authors += ", " + author[1]?.FirstOrDefault() + ". ";
                            if(author.Length == 3)
                            {
                                authors += author[2]?.FirstOrDefault() + ". ";
                            }
                            authors += author[0];
                        }
                    }
                }

                publication.AuthorsOrder = authors;
                if(year.HasValue)
                    publication.Date = new DateTime(year.Value, 1, 1);
                if(pagesFrom != -1 & pagesTo != -1)
                {
                    var pages = "";
                    pages += pagesFrom;
                    if (pagesFrom != pagesTo)
                        pages += "-" + pagesTo;
                    publication.Pages = pages;
                    publication.SizeOfPages = Math.Round((pagesTo - pagesFrom + 1) / 16.0, 1);
                }
                db.Publication.Add(publication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CurrentUser = userToAdd;
            }
            return View(publication);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "Id,Name,OtherAuthors,AuthorsOrder,Date,PagesFrom,PagesTo,PublicationType,Language," +
            "Link,Edition,Place,Magazine,DOI,Tome")] Publication publication, [Bind(Include = "authorsIds")] string[] authorsIds, int? year, bool? mainAuthorFromOthers,bool? changeMainAuthor, 
            [Bind(Include = "PagesFrom")] int pagesFrom = -1, [Bind(Include = "PagesTo")] int pagesTo = -1)
        {
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Where(x => x != nameof(PublicationType.Стаття))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = db.Users.Include(x => x.I18nUserInitials).Include(x=>x.Roles).Where(x => x.IsActive == true && !publication.User.Contains(x)).ToList();
            var roles = db.Roles.Where(x => x.Name == "Працівник"
            || x.Name == "Керівник кафедри"
            || x.Name == "Адміністрація ректорату"
            || x.Name == "Адміністрація деканату").Select(x => x.Id).ToList();
            var publicationFromDB = db.Publication.Find(publication.Id);
            var filtered = users
                .Where(y => y.IsActive && y.Roles.Any(z => roles.Contains(z.RoleId))).ToList();
            ViewBag.AllUsers = filtered.Select(x =>
            {
                    var name = x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language);
                    return new SelectListItem
                    {
                        Selected = false,
                        Text = String.Join(" ", name?.LastName,
                                                name?.FirstName,
                                                name?.FathersName),
                        Value = x.Id
                    };
            }).ToList();
            var userToAdd = new List<ApplicationUser>(); 
            publication.OtherAuthors = publication.OtherAuthors?.Trim();
            var authorsArr = publication.OtherAuthors?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (publication.OtherAuthors != null)
            {
                foreach (var item in authorsArr)
                {
                    var splitted = item.Split(' ');
                    if (splitted.Length == 3)
                    {
                        var firstName = splitted[1];
                        var lastName = splitted[0];
                        var middleName = splitted[2];
                        var user = db.Users.FirstOrDefault(x =>
                        x.I18nUserInitials.Any(y => y.FirstName == firstName && y.LastName == lastName && y.FathersName == middleName
                        && y.Language == publication.Language));
                        if (user != null)
                        {
                            userToAdd.Add(user);
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(publication.Name))
            {
                ModelState.AddModelError(nameof(publication.Name), "Введіть назву публікації");
            }
            if(!year.HasValue)
            {
                ModelState.AddModelError("year", "Введіть рік публікації");
            }
            if ((userToAdd == null || userToAdd.Count == 0) 
                && string.IsNullOrEmpty(publication.OtherAuthors)
                && (publicationFromDB.User == null || publicationFromDB.User.Count == 0))
            {
                ModelState.AddModelError(nameof(userToAdd), "Додайте авторів");
            }

            if (ModelState.IsValid)
            {
                publicationFromDB.Name = publication.Name;
                publicationFromDB.OtherAuthors = publication.OtherAuthors;
                publicationFromDB.PublicationType = publication.PublicationType;
                if (pagesFrom != -1 & pagesTo != -1)
                {
                    var pages = "";
                    pages += pagesFrom;
                    if (pagesFrom != pagesTo)
                        pages += "-" + pagesTo;
                    publicationFromDB.Pages = pages;
                    publicationFromDB.SizeOfPages = Math.Round((pagesTo - pagesFrom + 1) / 16.0, 1);
                }
                publicationFromDB.Language = publication.Language;
                publicationFromDB.PublicationType = publication.PublicationType;
                publicationFromDB.DOI = publication.DOI;
                publicationFromDB.Place = publication.Place;
                publicationFromDB.Magazine = publication.Magazine;
                publicationFromDB.Link = publication.Link;
                publicationFromDB.Edition = publication.Edition;
                publicationFromDB.Tome = publication.Tome;
                if (year.HasValue)
                    publicationFromDB.Date = new DateTime(year.Value, 1, 1);
                if (!string.IsNullOrEmpty(publication.OtherAuthors))
                {
                    var otherAuthors = authorsArr;
                    var authors = string.Empty;
                    for (int i = 0; i < otherAuthors.Length; i++)
                    {
                        var author = otherAuthors[i].Split();
                        if (author.Length >= 2)
                        {
                            authors += author[1]?.FirstOrDefault() + ". ";
                            if (author.Length == 3)
                            {
                                authors += author[2]?.FirstOrDefault() + ". ";
                            }
                            authors += author[0] + ", ";
                        }
                    }
                    if (authors.Length >= 2)
                    {
                        authors = authors.Remove(authors.Length - 2);
                    }
                    publicationFromDB.AuthorsOrder = authors;
                }
                if (userToAdd != null && userToAdd.Count != 0)
                {
                    publicationFromDB.User?.Clear();
                    foreach (var current in userToAdd)
                    {
                        publicationFromDB.User.Add(current);
                        current.Publication.Add(publicationFromDB);
                    }
                }
                if (mainAuthorFromOthers.Value)
                {
                    if (!string.IsNullOrEmpty(publication.OtherAuthors))
                    {
                        var value = authorsArr;
                        value = value[0].Split();
                        if (value.Length >= 2)
                        {
                            publicationFromDB.MainAuthor = value[0] + " " + value[1].FirstOrDefault() + ".";
                            if(value.Length == 3)
                            {
                                publicationFromDB.MainAuthor += " " + value[2].FirstOrDefault() + ".";
                            }
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                publication.User = publicationFromDB.User;
            }
            return View(publication);
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

        [HttpGet]
        public ActionResult DeleteUser(string userId, int publicationId)
        {
            var publication = db.Publication.Find(publicationId);
            var user = publication.User.Where(x => x.Id == userId).FirstOrDefault();
            if (user != null)
            {
                publication.User.Remove(user);
                user.Publication.Remove(publication);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Publications", new { id = publicationId });
        }

        private async Task FillAvailableDepartments(int? facultyId)
        {
            if (User.IsInRole(RoleNames.Superadmin) || User.IsInRole(RoleNames.RectorateAdmin))
            {
                ViewBag.AllCathedras = await _cathedraCrudService.GetAllAsync();
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
