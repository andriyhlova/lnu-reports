using System.Collections.Generic;
using System.Linq;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;
using ScientificReport.Services.Abstraction;

namespace ScientificReport.Services.Implementation
{
    public class ThemeOfScientificWorksService : ServiceBase<ThemeOfScientificWork, int>, IThemeOfScientificWorksService
    {
        public ThemeOfScientificWorksService(IThemeOfScientificWorkRepository repository) : base(repository)
        {
        }

        public IEnumerable<ThemeOfScientificWork> GetScientificThemesByCathedraId(int cathedraId)
        {
            return GetAllAsync().Result.Where(x => x.Cathedra.Id == cathedraId).ToList();
        }
    }
}