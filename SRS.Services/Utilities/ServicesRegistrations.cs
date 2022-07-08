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
            Bind<IBaseCrudService<CathedraModel>>().To<BaseCrudService<Cathedra, CathedraModel>>();
            Bind<IBaseCrudService<FacultyModel>>().To<BaseCrudService<Faculty, FacultyModel>>();
            Bind<IBaseCrudService<I18nUserInitialsModel>>().To<BaseCrudService<I18nUserInitials, I18nUserInitialsModel>>();
            Bind<IThemeOfScientificWorkService>().To<ThemeOfScientificWorkService>();
            Bind<IUserService>().To<UserService>();
            Bind<ISmtpClient>().To<SmtpClient>();
            Bind<IConfigurationProvider>().To<ConfigurationProvider>();
            Bind<IEmailService>().To<EmailService>();
        }
    }
}
