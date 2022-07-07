using Ninject.Modules;
using SRS.Domain.Entities;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Services.Utilities
{
    public class ServicesRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBaseCrudService<AcademicStatusModel>>().To<BaseCrudService<AcademicStatus, AcademicStatusModel>>();
            Bind<IThemeOfScientificWorkService>().To<ThemeOfScientificWorkService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
