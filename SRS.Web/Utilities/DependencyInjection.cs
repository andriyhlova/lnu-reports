using System.Collections.Generic;
using Ninject.Modules;
using SRS.Services.Utilities;

namespace SRS.Web.Utilities
{
    public static class DependencyInjection
    {
        public static INinjectModule[] GetRegistrations()
        {
            var registrations = new List<INinjectModule>
            {
                new Registrations()
            };
            registrations.AddRange(ServicesDependencyInjection.GetRegistrations());
            return registrations.ToArray();
        }
    }
}
