using System.Collections.Generic;
using Ninject.Modules;

namespace SRS.Repositories.Utilities
{
    public static class RepositoriesDependencyInjection
    {
        public static INinjectModule[] GetRegistrations()
        {
            var registrations = new List<INinjectModule>();
            registrations.Add(new RepositoriesRegistrations());
            return registrations.ToArray();
        }
    }
}
