using System.Data.Entity;
using System.Linq;
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

        protected override void UpdateRelatedEntities(Publication existingEntity, Publication newEntity)
        {
            var toDelete = existingEntity.User.Where(x => !newEntity.User.Any(y => y.Id == x.Id)).ToList();
            foreach (var user in toDelete)
            {
                existingEntity.User.Remove(user);
            }

            var toAdd = newEntity.User.Where(x => !existingEntity.User.Any(y => y.Id == x.Id)).ToList();
            foreach (var user in toAdd)
            {
                _context.Entry(user).State = EntityState.Unchanged;
                existingEntity.User.Add(user);
            }
        }
    }
}
