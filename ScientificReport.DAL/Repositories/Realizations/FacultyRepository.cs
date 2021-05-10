using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class FacultyRepository : BaseRepository<Faculty,int>, IFacultyRepository
    {
        public FacultyRepository(IDbContext context) : base(context)
        {
        }
    }
}
