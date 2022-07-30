using System.Data.Entity;
using SRS.Domain.Entities;
using SRS.Repositories.Context;

namespace SRS.Repositories.Implementations
{
    public class ReportRepository : BaseRepository<Report>
    {
        public ReportRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void AddRelatedEntities(Report entity)
        {
            foreach (var user in entity.PrintedPublication)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }

            foreach (var user in entity.RecomendedPublication)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }

            foreach (var user in entity.AcceptedToPrintPublication)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }
        }

        protected override void UpdateRelatedEntities(Report existingEntity, Report newEntity)
        {
            UpdateCollection(existingEntity.PrintedPublication, newEntity.PrintedPublication);
            UpdateCollection(existingEntity.RecomendedPublication, newEntity.RecomendedPublication);
            UpdateCollection(existingEntity.AcceptedToPrintPublication, newEntity.AcceptedToPrintPublication);
        }
    }
}
