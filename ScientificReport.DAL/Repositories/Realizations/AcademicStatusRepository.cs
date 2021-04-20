using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class AcademicStatusRepository:BaseRepository<AcademicStatus>, IAcademicStatusRepository
    {
        public AcademicStatusRepository(IDbContext context) : base(context)
        {
        }
    }
}
