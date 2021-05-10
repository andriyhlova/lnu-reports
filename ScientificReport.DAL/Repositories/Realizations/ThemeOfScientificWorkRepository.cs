using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class ThemeOfScientificWorkRepository : BaseRepository<ThemeOfScientificWork ,int>, IThemeOfScientificWorkRepository
    {
        public ThemeOfScientificWorkRepository(IDbContext context) : base(context)
        {
        }
    }
}
