using SRS.Domain.Entities;
using SRS.Repositories.Context;

namespace SRS.Repositories.Implementations
{
    public class JournalRepository : BaseRepository<Journal>
    {
        public JournalRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void AddRelatedEntities(Journal entity)
        {
            AddCollection(entity.JournalTypes);
        }

        protected override void UpdateRelatedEntities(Journal existingEntity, Journal newEntity)
        {
            UpdateCollection(existingEntity.JournalTypes, newEntity.JournalTypes);
        }
    }
}
