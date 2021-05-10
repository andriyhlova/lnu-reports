using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class ScienceDegreeRepository : BaseRepository<ScienceDegree ,int>, IScienceDegreeRepository
    {
        public ScienceDegreeRepository(IDbContext context) : base(context)
        {
        }
    }
}
