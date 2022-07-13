using MailKit.Net.Smtp;
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
            Bind<IBaseCrudService<ScienceDegreeModel>>().To<BaseCrudService<ScienceDegree, ScienceDegreeModel>>();
            Bind<IBaseCrudService<PositionModel>>().To<BaseCrudService<Position, PositionModel>>();
            Bind<IBaseCrudService<CathedraModel>>().To<BaseCrudService<Cathedra, CathedraModel>>();
            Bind<ICathedraService>().To<CathedraService>();
            Bind<IBaseCrudService<FacultyModel>>().To<BaseCrudService<Faculty, FacultyModel>>();
            Bind<IBaseCrudService<I18nUserInitialsModel>>().To<BaseCrudService<I18nUserInitials, I18nUserInitialsModel>>();
            Bind<IThemeOfScientificWorkService>().To<ThemeOfScientificWorkService>();
            Bind(typeof(IUserService<>)).To(typeof(UserService<>));
            Bind<IRoleService>().To<RoleService>();
            Bind<ISmtpClient>().To<SmtpClient>();
            Bind<IConfigurationProvider>().To<ConfigurationProvider>();
            Bind<IEmailService>().To<EmailService>();
        }
    }
}
