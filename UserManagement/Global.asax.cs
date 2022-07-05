using Ninject;
using Ninject.Web.Mvc;
using SRS.Repositories.Utilities;
using SRS.Services.Utilities;
using SRS.Web.Utilities;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserManagement
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var kernel = new StandardKernel(DependencyInjection.GetRegistrations());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
