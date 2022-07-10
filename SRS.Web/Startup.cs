using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SRS.Domain.Entities;
using SRS.Repositories.Context;
using SRS.Repositories.Migrations;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Providers;
using System;
using System.Data.Entity;
using System.Linq;

[assembly: OwinStartup(typeof(SRS.Web.Startup))]
namespace SRS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
 
        private void CreateRolesAndUsers()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            CreateSuperadmin(roleManager, userManager);
            CreateRectorateAdmin(roleManager);
            CreateDeaneryAdmin(roleManager);
            CreateCathedraAdmin(roleManager);
            CreateWorker(roleManager);

            RolesProvider.AllRoles = context.Roles.ToDictionary(x=>x.Id, x=>x.Name);
        }

        private void CreateSuperadmin(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!roleManager.RoleExists(RoleNames.Superadmin))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.Superadmin
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "superadmin@lnu.edu.ua",
                    Email = "superadmin@lnu.edu.ua",
                    BirthDate = DateTime.Now,
                    DefenseYear = DateTime.Now,
                    AwardingDate = DateTime.Now,
                    GraduationDate = DateTime.Now,
                    IsActive = true
                };
                var chkUser = userManager.Create(user, "qwerty1");
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, RoleNames.Superadmin);
                }
            }
        }

        private void CreateRectorateAdmin(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExists(RoleNames.RectorateAdmin))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.RectorateAdmin
                };
                roleManager.Create(role);
            }
        }

        private void CreateDeaneryAdmin(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExists(RoleNames.DeaneryAdmin))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.DeaneryAdmin
                };
                roleManager.Create(role);
            }
        }

        private void CreateCathedraAdmin(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExists(RoleNames.CathedraAdmin))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.CathedraAdmin
                };
                roleManager.Create(role);
            }
        }

        private void CreateWorker(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExists(RoleNames.Worker))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.Worker
                };
                roleManager.Create(role);
            }
        }

    }
}
