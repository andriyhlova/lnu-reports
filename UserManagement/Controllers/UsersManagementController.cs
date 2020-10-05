﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Superadmin, Адміністрація ректорату, Адміністрація деканату, Керівник кафедри")]
    public class UsersManagementController : Controller
    {
        private ApplicationDbContext DB = new ApplicationDbContext();
        //public ApplicationDbContext DB
        //{
        //    get
        //    {
        //        return db ?? new ApplicationDbContext();
        //    }
        //    private set
        //    {
        //        db = value;
        //    }
        //}

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UsersManagement
        public ActionResult Index(int? page, string sortOrder, int? cathedra, int? faculty)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            int cathedraNumber = cathedra ?? -1;
            int facultyNumber = faculty ?? -1;
            ViewBag.IsActiveSortParm = sortOrder == null ? "is_active_desc" : sortOrder == "is_active" ? "is_active_desc" : "is_active";
            List<ApplicationUser> list = DB.Users.ToList();
            var cathedas = DB.Cathedra.ToList();
            var faculties = DB.Faculty.ToList();
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            list.ForEach(x =>
            {
                map.Add(x.Id, x.Roles.Select(y => DB.Roles.Find(y.RoleId).Name).ToList());
            });
            ViewBag.AllCathedras = cathedas
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.ID.ToString()
                     }).ToList();
            ViewBag.AllFaculties = faculties
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.ID.ToString()
                     }).ToList();
            ViewBag.RolesForThisUser = map;
            var users = DB.Users
                .Where(x => cathedraNumber == -1 || (cathedraNumber != -1 && x.Cathedra.ID == cathedraNumber))
                .Where(x => facultyNumber == -1 || (facultyNumber != -1 && x.Cathedra.Faculty.ID == facultyNumber))
                .ToList();
            var currentUser = UserManager.FindByName(User.Identity.Name);
            if (User.IsInRole("Керівник кафедри"))
            {
                users = DB.Users
                .Where(x => x.Cathedra.ID == currentUser.Cathedra.ID).ToList();
            }
            if (User.IsInRole("Адміністрація деканату"))
            {
                users = DB.Users
                .Where(x => cathedraNumber == -1 || (cathedraNumber != -1 && x.Cathedra.ID == cathedraNumber))
                .Where(x => x.Cathedra.Faculty.ID == currentUser.Cathedra.Faculty.ID).ToList();

                ViewBag.AllCathedras = cathedas
                    .Where(x => x.Faculty.ID == currentUser.Cathedra.Faculty.ID)
                    .Select(x =>
                         new SelectListItem
                         {
                             Text = x.Name,
                             Value = x.ID.ToString()
                         }).ToList();
            }
            if (User.IsInRole("Адміністрація ректорату") || User.IsInRole("Superadmin"))
            {
                users = DB.Users
                    .Where(x => cathedraNumber == -1 || (cathedraNumber != -1 && x.Cathedra.ID == cathedraNumber))
                    .Where(x => facultyNumber == -1 || (facultyNumber != -1 && x.Cathedra.Faculty.ID == facultyNumber))
                    .ToList();
            }
            switch (sortOrder)
            {
                case "is_active_desc":
                    users = users.OrderByDescending(x => x.IsActive).ToList();
                    break;
                default:
                    users = users.OrderBy(x => x.IsActive).ToList();
                    break;
            }
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: UsersManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = DB.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var userRoles = applicationUser.Roles.Select(y => DB.Roles.Find(y.RoleId).Name).ToList();
            ViewBag.RolesForThisUser = userRoles;
            return View(applicationUser);
        }

        // GET: UsersManagement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = DB.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var userRoles = applicationUser.Roles.Select(y => DB.Roles.Find(y.RoleId).Name).ToList();
            ViewBag.RolesForThisUser = userRoles;
            if (User.IsInRole("Superadmin"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name)).Select(x => x.Name);
            }
            else if (User.IsInRole("Адміністрація ректорату"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Адміністрація деканату").Select(x => x.Name);
            }
            else if (User.IsInRole("Адміністрація деканату"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Керівник кафедри").Select(x => x.Name);
            }
            else if (User.IsInRole("Керівник кафедри"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Викладач").Select(x => x.Name);
            }
            ViewBag.AllCathedras = DB.Cathedra.ToList().Select(x => x.Name);
            ViewBag.AllAcademicStatuses = DB.AcademicStatus.ToList().Select(x => x.Value);
            ViewBag.AllScienceDegrees = DB.ScienceDegree.ToList().Select(x => x.Value);
            ViewBag.AllPositions = DB.Position.ToList().Select(x => x.Value);
            return View(applicationUser);
        }

        // POST: UsersManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName,FathersName," +
            "IsActive,PasswordHash,SecurityStamp,BirthDate,GraduationDate,AwardingDate,DefenseYear")] ApplicationUser applicationUser, 
            [Bind(Include = "RoleToAdd")] string roleToAdd)
        {
            var userRoles = applicationUser.Roles
                .Where(x => DB.Roles.Find(x.RoleId).Name != "Superadmin")
                .Select(y => DB.Roles.Find(y.RoleId).Name)
                .ToList();
            ViewBag.RolesForThisUser = userRoles;
            if (User.IsInRole("Superadmin"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Адміністрація ректорату").Select(x => x.Name);
            }
            else if (User.IsInRole("Адміністрація ректорату"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Адміністрація деканату").Select(x => x.Name);
            }
            else if (User.IsInRole("Адміністрація деканату"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Керівник кафедри").Select(x => x.Name);
            }
            else if (User.IsInRole("Керівник кафедри"))
            {
                ViewBag.AllRoles = DB.Roles.Where(x => !userRoles.Contains(x.Name) && x.Name == "Викладач").Select(x => x.Name);
            }
            var user = DB.Users.Find(applicationUser.Id);
            if (applicationUser.IsActive && (roleToAdd == null || roleToAdd.Equals("")) && user.Roles.Count == 0)
            {
                ViewBag.Error = "Impossible to make active user without role";
                return View(applicationUser);
            }
            if (roleToAdd != null && !roleToAdd.Equals("") && !user
                .Roles.Any(x => DB.Roles.Find(x.RoleId).Equals(roleToAdd)))
            {
                UserManager.AddToRole(applicationUser.Id, roleToAdd);
            }
            user.IsActive = applicationUser.IsActive;
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteRole(String userId, String roleName)
        {
            UserManager.RemoveFromRole(userId, roleName);
            return RedirectToAction("Edit", "UsersManagement", new { id = userId });
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing && _userManager != null)
            //{
            //    _userManager.Dispose();
            //    _userManager = null;
            //    db.Dispose();
            //    db = null;
            //}

            //base.Dispose(disposing);
        }
    }
}
