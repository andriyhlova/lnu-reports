using System.Data.Entity;
using SRS.Domain.Entities;
using SRS.Repositories.Context;

namespace SRS.Repositories.Implementations
{
    public class PublicationRepository : BaseRepository<Publication>
    {
        public PublicationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void AddRelatedEntities(Publication entity)
        {
            foreach (var user in entity.User)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }
        }
    }
}
