using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class PositionRepository : BaseRepository<Position ,int>, IPositionRepository
    {
        public PositionRepository(IDbContext context) : base(context)
        {
        }
    }
}
