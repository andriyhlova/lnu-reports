using ScientificReport.DAL.Repositories.Interfaces;
using ScientificReport.DAL.Repositories.Realizations;
using UserManagement.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UserManagement.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UserManagement.App_Start.NinjectWebCommon), "Stop")]

namespace UserManagement.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using AutoMapper;
    using ScientificReport.DAL;
    using ScientificReport.DAL.Abstraction;
    using ScientificReport.DAL.Implementation;
    using ScientificReport.DAL.Models;
    using ScientificReport.Services.Abstraction;
    using ScientificReport.Services.Implementation;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMapper>().ToMethod(ctx => new Mapper(AutoMapperConfig.Instance));
            kernel.Bind<IDbContext>().To<ApplicationDbContext>();
            kernel.Bind<IAcademicStatusRepository>().To<AcademicStatusRepository>();
            kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>();
            kernel.Bind<ICathedraReportRepository>().To<CathedraReportRepository>();
            kernel.Bind<ICathedraRepository>().To<CathedraRepository>();
            kernel.Bind<IFacultyRepository>().To<FacultyRepository>();
            kernel.Bind<IPositionRepository>().To<PositionRepository>();
            kernel.Bind<IPublicationRepository>().To<PublicationRepository>();
            kernel.Bind<IReportRepository>().To<ReportRepository>();
            kernel.Bind<IScienceDegreeRepository>().To<ScienceDegreeRepository>();
            kernel.Bind<IThemeOfScientificWorkRepository>().To<ThemeOfScientificWorkRepository>();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<ICathedraRepository>().To<CathedraRepository>();
            kernel.Bind<IThemeOfScientificWorkRepository>().To<ThemeOfScientificWorkRepository>();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ICathedraService>().To<CathedraService>();
            kernel.Bind<IThemeOfScientificWorksService>().To<ThemeOfScientificWorksService>();
            kernel.Bind<ICathedraReportService>().To<CathedraReportService>();
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<IPublicationService>().To<PublicationService>();
            kernel.Bind<IReportService>().To<ReportService>();
        }        
    }
}
