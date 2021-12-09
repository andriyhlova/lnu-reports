using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Models.PublicationDB;

namespace UserManagement.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //public static ApplicationDbContext db
        //{
        //    get
        //    {
        //        return DB ?? new ApplicationDbContext();
        //    }
        //    private set
        //    {
        //        DB = value;
        //    }
        //}

        private UserManager<ApplicationUser> UserManager;
        
        public PublicationsController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
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
            var allPublications = db.Publication
                .Include(x=> x.User.Select(y => y.Cathedra.Faculty))
                .Where(x => !hasUser || (hasUser && x.User.Any(y => y.Id == user)))
                .Where(x => cathedraNumber == -1 || (cathedraNumber != -1 && x.User.Any(y => y.Cathedra.ID == cathedraNumber)))
                .Where(x => facultyNumber == -1 || (facultyNumber != -1 && x.User.Any(y => y.Cathedra.Faculty.ID == facultyNumber)))
                .OrderByDescending(x=> x.Date)
                .ToList();
            allPublications = allPublications
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && Convert.ToDateTime(x.Date) >= DateTime.Parse(dateFromVerified)))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && Convert.ToDateTime(x.Date) <= DateTime.Parse(dateToVerified)))
                .ToList();
            return GetRightPublicationView(allPublications, isMineWihoutNull, pageNumber, pageSize, searchString);
        }

        private void PutCathedraAndFacultyIntoViewBag(bool isMine = false)
        {
            var users = db.Users.Include(x=>x.I18nUserInitials).Include(x=>x.Cathedra.Faculty).Where(x => x.IsActive == true && x.I18nUserInitials.Any(y=>y.Language==Language.UA)).ToList();
            var cathedas = db.Cathedra.OrderBy(x => x.Name).ToList();
            var faculties = db.Faculty.OrderBy(x => x.Name).ToList();
            var currentUser = UserManager.FindByName(User.Identity.Name);
            //UserManager.IsInRole(x.Id, "Працівник") || UserManager.IsInRole(x.Id, "Керівник кафедри")
            //    || UserManager.IsInRole(x.Id, "Адміністрація ректорату") || UserManager.IsInRole(x.Id, "Адміністрація деканату")
            if (isMine && !User.IsInRole("Superadmin") && !User.IsInRole("Адміністрація ректорату"))
            {
                if(User.IsInRole("Адміністрація деканату"))
                {
                    cathedas = cathedas.Where(x => x.Faculty.ID == currentUser.Cathedra.Faculty.ID).ToList();
                    users = users.Where(x=>x.Cathedra.Faculty.ID == currentUser.Cathedra.Faculty.ID).ToList();
                }
                else if (User.IsInRole("Керівник кафедри"))
                {
                    users = users.Where(x => x.Cathedra.ID == currentUser.Cathedra.ID).ToList();
                }
            }
            ViewBag.AllUsers = users;
            ViewBag.AllCathedras = cathedas;
            ViewBag.AllFaculties = faculties
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.ID.ToString()
                     }).ToList();
        }

        private ActionResult GetRightPublicationView(List<Publication> allPublications, bool isMineWihoutNull,
                                                        int pageNumber, int pageSize, string searchString)
        {
            var publications = allPublications.ToList();
            var currentUser = UserManager.FindByName(User.Identity.Name);
            var search = searchString?.ToLower();
            if (!String.IsNullOrEmpty(search))
            {
                publications = publications.Where(s => s.Name.ToLower().Contains(search))
                    .ToList();
            }
            if (!User.IsInRole("Superadmin") && !User.IsInRole("Адміністрація ректорату"))
            {
                if (User.IsInRole("Адміністрація деканату"))
                {
                    publications = publications.Where(x => x.User.Any(y => y.UserName == User.Identity.Name
                    || y.Cathedra.Faculty.ID == currentUser.Cathedra.Faculty.ID))
                    .ToList();
                }
                else if (User.IsInRole("Керівник кафедри"))
                {
                    publications = publications.Where(x => x.User.Any(y => y.UserName == User.Identity.Name
                    || y.Cathedra.ID == currentUser.Cathedra.ID))
                    .ToList();
                }
                else if (User.IsInRole("Працівник"))
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
            Publication publication = db.Publication.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }

            ViewBag.PublicationUsers = (publication.AuthorsOrder ?? publication.OtherAuthors)?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return View(publication);
        }

        // GET: Publications/Create
        public ActionResult Create(string language)
        {
            var languageVerified = language == null || language == "" ? "UA" : language;
            var users = db.Users.Include(x=>x.I18nUserInitials).Include(x=>x.Roles).Where(x => x.IsActive == true).ToList();
            var roles = db.Roles.Where(x=> x.Name == "Працівник"
            || x.Name == "Керівник кафедри"
            || x.Name == "Адміністрація ректорату"
            || x.Name == "Адміністрація деканату").Select(x=>x.Id).ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Where(x => x != nameof(PublicationType.Стаття))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' ').Replace(" які",", які"), Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllUsers = users
                .Where(x => x.IsActive && x.Roles.Any(y=>roles.Contains(y.RoleId)))
                .Select(x => {
                    var name = x.I18nUserInitials.FirstOrDefault(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified));
                    return new SelectListItem
                    {
                        Selected = false,
                        Text = string.Join(" ", name?.LastName,
                                                name?.FirstName,
                                                name?.FathersName),
                        Value = x.Id
                    };
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
        public ActionResult Create([Bind(Include = "ID,Name,OtherAuthors,PagesFrom,PagesTo,PublicationType,Place," +
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

        // GET: Publications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publication.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }            
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Where(x => x != nameof(PublicationType.Стаття))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' ').Replace(" які", ", які"), Value = x }).ToList();            
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
        public ActionResult Edit([Bind(Include = "ID,Name,OtherAuthors,AuthorsOrder,Date,PagesFrom,PagesTo,PublicationType,Language," +
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
            var publicationFromDB = db.Publication.Find(publication.ID);
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

        // GET: Publications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publication.Find(id);
            if (publication.AcceptedToPrintPublicationReport.Union(publication.PrintedPublicationReport.Union(publication.RecomendedPublicationReport)).Count() > 0)
            {
                ViewBag.Exists = "Ця публікація включена в звіт. Неможливо видалити.";
            }
            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publication publication = db.Publication.Find(id);
            if (publication.AcceptedToPrintPublicationReport.Union(publication.PrintedPublicationReport.Union(publication.RecomendedPublicationReport)).Count() > 0)
            {
                return RedirectToAction("Index");
            }
            db.Publication.Remove(publication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteUser(String userId, int publicationId)
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
        protected override void Dispose(bool disposing)
        {
            //if (disposing && DB != null)
            //{
            //    DB.Dispose();
            //    DB = null;
            //}

            //base.Dispose(disposing);
        }
    }
}
