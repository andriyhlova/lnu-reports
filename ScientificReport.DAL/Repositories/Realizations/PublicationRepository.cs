using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class PublicationRepository : BaseRepository<Publication ,int>, IPublicationRepository
    {
        public PublicationRepository(IDbContext context) : base(context)
        {
        }
    }
}
