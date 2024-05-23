using Ninject.Modules;
using SRS.Domain.Entities;
using SRS.Repositories.Context;
using SRS.Repositories.Implementations;
using SRS.Repositories.Interfaces;

namespace SRS.Repositories.Utilities
{
    public class RepositoriesRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>));
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IBaseRepository<Publication>>().To<PublicationRepository>();
            Bind<IBaseRepository<Report>>().To<ReportRepository>();
            Bind<IBaseRepository<CathedraReport>>().To<CathedraReportRepository>();
            Bind<IBaseRepository<ThemeOfScientificWork>>().To<ThemeOfScientificWorkRepository>();
            Bind<IBaseRepository<Journal>>().To<JournalRepository>();
            Bind<ApplicationDbContext>().ToSelf();
        }
    }
}
