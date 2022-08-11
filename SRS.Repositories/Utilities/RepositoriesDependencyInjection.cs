using System.Collections.Generic;
using Ninject.Modules;

namespace SRS.Repositories.Utilities
{
    public static class RepositoriesDependencyInjection
    {
        public static INinjectModule[] GetRegistrations()
        {
            var registrations = new List<INinjectModule>
            {
                new RepositoriesRegistrations()
            };
            return registrations.ToArray();
        }
    }
}
