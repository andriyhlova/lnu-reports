using Ninject.Modules;
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
            Bind<ApplicationDbContext>().ToSelf();
        }
    }
}
