using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class CathedraRepository : BaseRepository<Cathedra ,int>, ICathedraRepository
    {
        public CathedraRepository(IDbContext context) : base(context)
        {
        }
    }
}
