using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using ScientificReport.DAL;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity.Owin;
using ScientificReport.DAL.Abstraction;

namespace UserManagement.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        private ApplicationUserManager _userManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private readonly IUnitOfWork _db;

        public PublicationsController(IUnitOfWork db)
        {
            this._db = db;
        }
        
        // GET: Publications
        public ActionResult Index(int? page, bool? isMine, string searchString, string dateFrom, string dateTo, int? cathedra, int? faculty, string user)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            bool isMineWihoutNull = isMine ?? true;
            int cathedraNumber = cathedra ?? -1;
            int facultyNumber = faculty ?? -1;
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.isMine = isMineWihoutNull;
            ViewBag.cathedra = cathedra;
            ViewBag.faculty = faculty;
            ViewBag.user = user;
            ViewBag.searchString = searchString;
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            PutCathedraAndFacultyIntoViewBag(isMineWihoutNull);
            bool hasUser = !string.IsNullOrEmpty(user);
            var allPublications = _db.Publications.GetAllAsync().Result
                .Where(x => !hasUser || (hasUser && x.User.Any(y => y.Id == user)))
                .Where(x => cathedraNumber == -1 || (cathedraNumber != -1 && x.User.Any(y => y.Cathedra.Id == cathedraNumber)))
                .Where(x => facultyNumber == -1 || (facultyNumber != -1 && x.User.Any(y => y.Cathedra.Faculty.Id == facultyNumber)))
                .ToList();
            allPublications = allPublications
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && Convert.ToDateTime(x.Date) >= DateTime.Parse(dateFromVerified)))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && Convert.ToDateTime(x.Date) <= DateTime.Parse(dateToVerified)))
                .ToList();
            return GetRightPublicationView(allPublications, isMineWihoutNull, pageNumber, pageSize, searchString);
        }

        private void PutCathedraAndFacultyIntoViewBag(bool isMine = false)
        {
            var users = _db.Users.GetAllAsync().Result.Where(x => x.IsActive == true && x.I18nUserInitials.Any(y=>y.Language==Language.UA)).ToList();
            var cathedas = _db.Cathedras.GetAllAsync().Result.OrderBy(x => x.Name).ToList();
            var faculties = _db.Faculties.GetAllAsync().Result.OrderBy(x => x.Name).ToList();
            var currentUser = _userManager.FindByName(User.Identity.Name);
            //UserManager.IsInRole(x.Id, "Працівник") || UserManager.IsInRole(x.Id, "Керівник кафедри")
            //    || UserManager.IsInRole(x.Id, "Адміністрація ректорату") || UserManager.IsInRole(x.Id, "Адміністрація деканату")
            if (isMine)
            {
                if(User.IsInRole("Адміністрація деканату"))
                {
                    cathedas = cathedas.Where(x => x.Faculty.Id == currentUser.Cathedra.Faculty.Id).ToList();
                    users = users.Where(x=>x.Cathedra.Faculty.Id == currentUser.Cathedra.Faculty.Id).ToList();
                }
                else if (User.IsInRole("Керівник кафедри"))
                {
                    users = users.Where(x => x.Cathedra.Id == currentUser.Cathedra.Id).ToList();
                }
            }
            ViewBag.AllUsers = users
                .Select(x =>
                {
                    var init = x.I18nUserInitials.First(y => y.Language == Language.UA);
                    return new SelectListItem
                    {
                        Text = String.Join(" ", init.LastName,
                                                init.FirstName,
                                                init.FathersName),
                        Value = x.Id
                    };
                }).ToList();
            ViewBag.AllCathedras = cathedas
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.Id.ToString()
                     }).ToList();
            ViewBag.AllFaculties = faculties
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.Id.ToString()
                     }).ToList();
        }

        private ActionResult GetRightPublicationView(List<Publication> allPublications, bool isMineWihoutNull,
                                                        int pageNumber, int pageSize, string searchString)
        {
            var publications = allPublications.ToList();
            var currentUser = _userManager.FindByName(User.Identity.Name);
            if (!String.IsNullOrEmpty(searchString))
            {
                publications = publications.Where(s => s.Name.Contains(searchString))
                    .ToList();
            }
            else if (isMineWihoutNull)
            {
                if (User.IsInRole("Адміністрація деканату"))
                {
                    publications = publications.Where(x => x.User.Any(y => y.UserName == User.Identity.Name 
                    || y.Cathedra.Faculty.Id == currentUser.Cathedra.Faculty.Id))
                    .ToList();
                }
                else if (User.IsInRole("Керівник кафедри"))
                {
                    publications = publications.Where(x => x.User.Any(y => y.UserName == User.Identity.Name
                    || y.Cathedra.Id == currentUser.Cathedra.Id))
                    .ToList();
                }
                else if(User.IsInRole("Працівник"))
                {
                    publications = publications.Where(x => x.User.Any(y => y.UserName == User.Identity.Name))
                    .ToList();
                }
            }

            return View(publications
                .ToPagedList(pageNumber, pageSize));
        }

        // GET: Publications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = _db.Publications.FindByIdAsync(id.Value).Result;
            if (publication == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublicationUsers = String.Join(", ", publication.User
                .Select(x => String.Join(" ", x.I18nUserInitials.Single(y => y.Language == publication.Language).LastName,
                                              x.I18nUserInitials.Single(y => y.Language == publication.Language).FirstName,
                                              x.I18nUserInitials.Single(y => y.Language == publication.Language).FathersName))
                    .ToList());
            return View(publication);
        }

        // GET: Publications/Create
        public ActionResult Create(string language)
        {
            var languageVerified = language == null || language == "" ? "UA" : language;
            var users = _db.Users.GetAllAsync().Result.Where(x => x.IsActive == true).ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' ').Replace(" які",", які"), Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllUsers = users
                .Where(x => _userManager.IsInRole(x.Id, "Працівник") || _userManager.IsInRole(x.Id, "Керівник кафедри")
                || _userManager.IsInRole(x.Id, "Адміністрація ректорату") || _userManager.IsInRole(x.Id, "Адміністрація деканату"))
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.Single(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified)).LastName,
                                                 x.I18nUserInitials.Single(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified)).FirstName,
                                                 x.I18nUserInitials.Single(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified)).FathersName),
                         Value = x.Id
                     })
                    .ToList();
            ViewBag.CurrentUser = users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(x => x.Id).ToList();
            return View();
        }

        // POST: Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,OtherAuthors,PagesFrom,PagesTo,PublicationType,Place," +
            "MainAuthor,IsMainAuthorRegistered,Language,Link,Edition,Magazine,DOI,Tome")] Publication publication, int? year,
            [Bind(Include = "IsMainAuthorRegistered")] bool? mainAuthorFromOthers, [Bind(Include = "authorsOrder")] string[] authorsOrder, [Bind(Include = "PagesFrom")] int pagesFrom = -1,
            [Bind(Include = "PagesTo")] int pagesTo = -1)
        {
            var users = _db.Users.GetAllAsync().Result.Where(x => x.IsActive == true).ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var filtered = users
                .Where(x => _userManager.IsInRole(x.Id, "Працівник") || _userManager.IsInRole(x.Id, "Керівник кафедри")
                                                                     || _userManager.IsInRole(x.Id, "Адміністрація ректорату") || _userManager.IsInRole(x.Id, "Адміністрація деканату")).ToList();
            ViewBag.AllUsers = filtered
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).LastName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FirstName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FathersName),
                         Value = x.Id
                     })
                    .ToList();
            var userToAdd = new List<string>();
            if (authorsOrder != null & !String.IsNullOrEmpty(authorsOrder[0]) & authorsOrder[0].Split(',').Length!=0)
            {
                foreach (var i in authorsOrder[0].Split(','))
                {
                    userToAdd.Add(filtered[Convert.ToInt32(i)].Id);
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
                    var publicationExists = _db.Publications.GetAllAsync().Result
                        .Any(x =>
                        x.Name == publication.Name
                        && userToAdd.All(y => x.User.Select(z => z.Id).Contains(y))
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
                            var user = _db.Users.FindByIdAsync(current).Result;
                            publication.User.Add(user);
                            user.Publication.Add(publication);
                        }
                    }
                }
                var authors = string.Empty;
                if (mainAuthorFromOthers.Value)
                {
                    if(!string.IsNullOrEmpty(publication.OtherAuthors))
                    {
                        var values = publication.OtherAuthors.Split(',');
                        values = values[0].Split();
                        if(values.Length == 3)
                        {
                            publication.MainAuthor = values[2] + " " + values[0] + " " + values[1];
                            authors += values[0] + " " + values[1] + " " + values[2];
                        }
                    }
                }
                else
                {
                    var user = _db.Users.FindByIdAsync(userToAdd[0]).Result;
                    var initials = user.I18nUserInitials.First(x => x.Language == publication.Language);
                    var lastName = initials.LastName ?? string.Empty;
                    var firstname = initials.FirstName != null && initials.FirstName.Length > 1
                        ? initials.FirstName.Substring(0, 1).ToUpper()
                        : string.Empty;
                    var fatherName = initials.FathersName != null && initials.FathersName.Length > 1
                        ? initials.FathersName.Substring(0, 1).ToUpper()
                        : string.Empty;
                    publication.MainAuthor = lastName + " " + firstname + ". " + fatherName + ". ";
                }
                foreach (var user in publication.User)
                {
                    if(!string.IsNullOrEmpty(authors))
                    {
                        authors += ", ";
                    }
                    var initials = (user?.I18nUserInitials).First(x => x.Language == publication.Language);
                    var lastName = initials.LastName ?? string.Empty;
                    var firstname = initials.FirstName != null && initials.FirstName.Length > 1
                        ? initials.FirstName.Substring(0, 1).ToUpper()
                        : string.Empty;
                    var fatherName = initials.FathersName != null && initials.FathersName.Length > 1
                        ? initials.FathersName.Substring(0, 1).ToUpper()
                        : string.Empty;

                    authors += firstname
                        + ". " + fatherName
                        + ". " + lastName;
                }
                if(!string.IsNullOrEmpty(publication.OtherAuthors))
                {
                    var otherAuthors = publication.OtherAuthors.Split(',');
                    var start = 0;
                    if (mainAuthorFromOthers.Value)
                        start = 1;
                    for (int i = start; i < otherAuthors.Length; i++)
                    {
                        var author = otherAuthors[i].Split();
                        if(author.Length == 3)
                            authors += ", " + author[0] + " " + author[1] + " " + author[2];
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
                _db.Publications.CreateAsync(publication).ConfigureAwait(false);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CurrentUser = userToAdd;
            }
            return View(publication);
        }        

        // GET: Publications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = _db.Publications.FindByIdAsync(id.Value).Result;
            if (publication == null)
            {
                return HttpNotFound();
            }            
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' ').Replace(" які", ", які"), Value = x }).ToList();            
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = _db.Users.GetAllAsync().Result.ToList();
            ViewBag.AllUsers = users
                .Where(x => _userManager.IsInRole(x.Id, "Працівник") || _userManager.IsInRole(x.Id, "Адміністрація ректорату") ||
                            _userManager.IsInRole(x.Id, "Адміністрація деканату") || _userManager.IsInRole(x.Id, "Керівник кафедри"))
                .Where(y => !publication.User.Contains(y) && y.IsActive)
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).LastName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FirstName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FathersName),
                         Value = x.Id
                     })
                    .ToList();
            ViewBag.PagesFrom = 0;
            ViewBag.PagesTo = 0;
            if(publication.Pages != null)
            {
                var pages = publication.Pages.Split('-');
                ViewBag.PagesFrom = pages[0];
                if (pages.Length == 2)
                    ViewBag.PagesTo = pages[1];
                else if (pages.Length == 1)
                    ViewBag.PagesTo = pages[0];
            }
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OtherAuthors,AuthorsOrder,Date,PagesFrom,PagesTo,PublicationType,Language," +
            "Link,Edition,Place,Magazine,DOI,Tome")] Publication publication, [Bind(Include = "authorsIds")] string[] authorsIds, int? year, bool? mainAuthorFromOthers,bool? changeMainAuthor, 
            [Bind(Include = "PagesFrom")] int pagesFrom = -1, [Bind(Include = "PagesTo")] int pagesTo = -1)
        {
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = _db.Users.GetAllAsync().Result.Where(x => x.IsActive == true && !publication.User.Contains(x)).ToList();
            var publicationFromDB = _db.Publications.FindByIdAsync(publication.Id).Result;
            var filtered = users
                .Where(y => !publicationFromDB.User.Contains(y))
                .Where(x => (_userManager.IsInRole(x.Id, "Працівник") || _userManager.IsInRole(x.Id, "Керівник кафедри")
                                                                      || _userManager.IsInRole(x.Id, "Адміністрація ректорату") || _userManager.IsInRole(x.Id, "Адміністрація деканату"))).ToList();
            ViewBag.AllUsers = filtered.Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).LastName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FirstName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FathersName),
                         Value = x.Id
                     })
                    .ToList();
            var userToAdd = new List<string>();
            if (authorsIds != null & !String.IsNullOrEmpty(authorsIds[0]) & authorsIds[0].Split(',').Length != 0)
            {
                foreach (var i in authorsIds[0].Split(','))
                {
                    userToAdd.Add(filtered[Convert.ToInt32(i)].Id);
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
                if(userToAdd != null)
                {
                    var publicationExists = _db.Publications.GetAllAsync().Result
                        .Any(x =>
                        x.Id != publication.Id
                        && x.Name == publication.Name
                        && userToAdd.All(y => x.User.Select(z => z.Id).Contains(y))
                        && x.PublicationType == publication.PublicationType
                        );
                    if (publicationExists)
                    {
                        ModelState.AddModelError("", "Така публікація вже існує");
                        return View(publication);
                    }
                }
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
                if (publication.AuthorsOrder != null)
                    publicationFromDB.AuthorsOrder = publication.AuthorsOrder;
                if (userToAdd != null && userToAdd.Count != 0)
                {
                    foreach (var current in userToAdd)
                    {
                        var user = _db.Users.FindByIdAsync(current).Result;
                        publicationFromDB.User.Add(user);
                        user.Publication.Add(publicationFromDB);
                    }
                }
                if (mainAuthorFromOthers.Value)
                {
                    if (!string.IsNullOrEmpty(publication.OtherAuthors))
                    {
                        var value = publication.OtherAuthors.Split(',');
                        value = value[0].Split();
                        if (value.Length == 3)
                            publicationFromDB.MainAuthor = value[2] + " " + value[0] + " " + value[1];
                    }
                }
                else if (changeMainAuthor.Value)
                {
                    if(userToAdd != null && userToAdd.Count > 0)
                    {
                        var user = _db.Users.FindByIdAsync(userToAdd[0]).Result;
                        var initials = user.I18nUserInitials.Where(x => x.Language == publication.Language).First();
                        publicationFromDB.MainAuthor = initials.LastName + " " + initials.FirstName.Substring(0, 1).ToUpper() + ". " + initials.FathersName.Substring(0, 1).ToUpper() + ". ";
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                publication.User = publicationFromDB.User;
            }
            return View(publication);
        }

        // GET: Publications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = _db.Publications.FindByIdAsync(id.Value).Result;
            if (publication.AcceptedToPrintPublicationReport.Union(publication.PrintedPublicationReport.Union(publication.RecomendedPublicationReport)).Any())
            {
                ViewBag.Exists = "Ця публікація включена в звіт. Неможливо видалити.";
            }

            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publication publication = _db.Publications.FindByIdAsync(id).Result;
            if (publication.AcceptedToPrintPublicationReport.Union(publication.PrintedPublicationReport.Union(publication.RecomendedPublicationReport)).Any())
            {
                return RedirectToAction("Index");
            }
            _db.Publications.RemoveAsync(publication).ConfigureAwait(false);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteUser(String userId, int publicationId)
        {
            var publication = _db.Publications.FindByIdAsync(publicationId).Result;
            var user = publication.User.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                publication.User.Remove(user);
                user.Publication.Remove(publication);
                _db.SaveChanges();
            }
            return RedirectToAction("Edit", "Publications", new { id = publicationId });
        }
    }
}
