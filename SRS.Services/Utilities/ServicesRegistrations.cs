using MailKit.Net.Smtp;
using Ninject.Modules;
using SRS.Domain.Entities;
using SRS.Services.Implementations;
using SRS.Services.Implementations.ReportGeneration;
using SRS.Services.Interfaces;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models;
using SRS.Services.Models.JournalModels;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Models.ReportGenerationModels.CathedraReport;
using SRS.Services.Models.ReportGenerationModels.Report;
using SRS.Services.Models.ThemeOfScientificWorkModels;
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
            Bind<IBaseCrudService<DegreeModel>>().To<BaseCrudService<Degree, DegreeModel>>();
            Bind<IBaseCrudService<AcademicStatusModel>>().To<BaseCrudService<AcademicStatus, AcademicStatusModel>>();
            Bind<IBaseCrudService<PositionModel>>().To<BaseCrudService<Position, PositionModel>>();
            Bind<IBaseCrudService<CathedraModel>>().To<BaseCrudService<Cathedra, CathedraModel>>();
            Bind<IBaseCrudService<ThemeOfScientificWorkModel>>().To<BaseCrudService<ThemeOfScientificWork, ThemeOfScientificWorkModel>>();
            Bind<IBaseCrudService<FacultyModel>>().To<BaseCrudService<Faculty, FacultyModel>>();
            Bind<IBaseCrudService<I18nUserInitialsModel>>().To<BaseCrudService<I18nUserInitials, I18nUserInitialsModel>>();
            Bind<IBaseCrudService<PublicationModel>>().To<BaseCrudService<Publication, PublicationModel>>();
            Bind<IBaseCrudService<JournalModel>>().To<BaseCrudService<Journal, JournalModel>>();
            Bind<IBaseCrudService<JournalTypeModel>>().To<BaseCrudService<JournalType, JournalTypeModel>>();
        }

        private void AddCustomServices()
        {
            Bind<ICathedraService>().To<CathedraService>();
            Bind<IThemeOfScientificWorkService>().To<ThemeOfScientificWorkService>();
            Bind<IDegreeService>().To<DegreeService>();
            Bind<IAcademicStatusService>().To<AcademicStatusService>();
            Bind<IHonoraryTitleService>().To<HonoraryTitleService>();
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
            Bind<IReportTemplateService>().To<ReportTemplateService>();
            Bind<ICathedraReportTemplateService>().To<CathedraReportTemplateService>();
            Bind<IHtmlCompiler>().To<HandlebarsHtmlCompiler>();
            Bind<IHtmlReportBuilderService<ReportTemplateModel>>().To<HtmlReportBuilderService<ReportTemplateModel>>();
            Bind<IHtmlReportBuilderService<CathedraReportTemplateModel>>().To<HtmlReportBuilderService<CathedraReportTemplateModel>>();
            Bind<ITexReportBuilderService>().To<TexReportBuilderService>();
            Bind<IWordReportBuilderService>().To<WordReportBuilderService>();
            Bind<IBibliographyService>().To<BibliographyService>();
        }
    }
}
