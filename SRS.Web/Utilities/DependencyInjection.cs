using Ninject.Modules;
using SRS.Services.Utilities;
using System.Collections.Generic;

namespace SRS.Web.Utilities
{
    public static class DependencyInjection
    {
        public static INinjectModule[] GetRegistrations()
        {
            var registrations = new List<INinjectModule>();
            registrations.Add(new Registrations());
            registrations.AddRange(ServicesDependencyInjection.GetRegistrations());
            return registrations.ToArray();
        }
    }
}
