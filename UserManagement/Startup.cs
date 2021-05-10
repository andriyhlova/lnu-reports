using Microsoft.Owin;
using Owin;
using ScientificReport.DAL;
using System.Data.Entity;
using ScientificReport.DAL.Migrations;

[assembly: OwinStartup(typeof(UserManagement.Startup))]
namespace UserManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
