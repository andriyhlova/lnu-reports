using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScientificReport.DAL.Configuration;
using ScientificReport.DAL.Models;

namespace ScientificReport.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists(Roles.Employee))
            {
                roleManager.Create(new IdentityRole(Roles.Employee));
            }

            if (!roleManager.RoleExists(Roles.CathedraManager))
            {
                roleManager.Create(new IdentityRole(Roles.CathedraManager));
            }

            if (!roleManager.RoleExists(Roles.DekanatAdministration))
            {
                roleManager.Create(new IdentityRole(Roles.DekanatAdministration));
            }

            if (!roleManager.RoleExists(Roles.RectoratAdministration))
            {
                roleManager.Create(new IdentityRole(Roles.RectoratAdministration));
            }

            if (!roleManager.RoleExists(Roles.SuperAdmin))
            {
                roleManager.Create(new IdentityRole(Roles.SuperAdmin));
            }
  
            if (userManager.FindByEmailAsync("superadmin@lnu.edu.ua").Result == null)
            {
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

                const string userPwd = "qwerty1";

                var chkUser = userManager.Create(user, userPwd);

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, Roles.SuperAdmin);
                }
            }
        }
    }
}
