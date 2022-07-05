using System.Collections.Generic;
using Ninject.Modules;
using SRS.Repositories.Utilities;

namespace SRS.Services.Utilities
{
    public static class ServicesDependencyInjection
    {
        public static INinjectModule[] GetRegistrations()
        {
            var registrations = new List<INinjectModule>();
            registrations.Add(new ServicesRegistrations());
            registrations.AddRange(RepositoriesDependencyInjection.GetRegistrations());
            return registrations.ToArray();
        }
    }
}
