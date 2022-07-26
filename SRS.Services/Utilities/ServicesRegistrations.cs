using MailKit.Net.Smtp;
using Ninject.Modules;
using SRS.Domain.Entities;
using SRS.Services.Implementations;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Utilities
{
    public class ServicesRegistrations : NinjectModule
    {
        public override void Load()
        {
            AddBaseCrudServices();
            AddCustomServices();
        }

        private void AddBaseCrudServices()
        {
            Bind<IBaseCrudService<AcademicStatusModel>>().To<BaseCrudService<AcademicStatus, AcademicStatusModel>>();
            Bind<IBaseCrudService<ScienceDegreeModel>>().To<BaseCrudService<ScienceDegree, ScienceDegreeModel>>();
            Bind<IBaseCrudService<PositionModel>>().To<BaseCrudService<Position, PositionModel>>();
            Bind<IBaseCrudService<CathedraModel>>().To<BaseCrudService<Cathedra, CathedraModel>>();
            Bind<IBaseCrudService<ThemeOfScientificWorkModel>>().To<BaseCrudService<ThemeOfScientificWork, ThemeOfScientificWorkModel>>();
            Bind<IBaseCrudService<FacultyModel>>().To<BaseCrudService<Faculty, FacultyModel>>();
            Bind<IBaseCrudService<I18nUserInitialsModel>>().To<BaseCrudService<I18nUserInitials, I18nUserInitialsModel>>();
            Bind<IBaseCrudService<PublicationModel>>().To<BaseCrudService<Publication, PublicationModel>>();
            Bind<IBaseCrudService<JournalModel>>().To<BaseCrudService<Journal, JournalModel>>();
        }

        private void AddCustomServices()
        {
            Bind<ICathedraService>().To<CathedraService>();
            Bind<IThemeOfScientificWorkService>().To<ThemeOfScientificWorkService>();
            Bind<IAcademicStatusService>().To<AcademicStatusService>();
            Bind<IPublicationService>().To<PublicationService>();
            Bind<IJournalService>().To<JournalService>();
            Bind<IReportService>().To<ReportService>();
            Bind<ICathedraReportService>().To<CathedraReportService>();
            Bind(typeof(IUserService<>)).To(typeof(UserService<>));
            Bind<IRoleService>().To<RoleService>();
            Bind<IConfigurationProvider>().To<ConfigurationProvider>();
            Bind<ISmtpClient>().To<SmtpClient>();
            Bind<IEmailService>().To<EmailService>();
            Bind<IRoleActionService>().To<RoleActionService>();
        }
    }
}
