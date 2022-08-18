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
            AddCollection(entity.PrintedPublication);
            AddCollection(entity.RecomendedPublication);
            AddCollection(entity.AcceptedToPrintPublication);
            AddCollection(entity.ThemeOfScientificWorks);
        }

        protected override void UpdateRelatedEntities(Report existingEntity, Report newEntity)
        {
            UpdateCollection(existingEntity.PrintedPublication, newEntity.PrintedPublication);
            UpdateCollection(existingEntity.RecomendedPublication, newEntity.RecomendedPublication);
            UpdateCollection(existingEntity.AcceptedToPrintPublication, newEntity.AcceptedToPrintPublication);
            UpdateCollection(existingEntity.ThemeOfScientificWorks, newEntity.ThemeOfScientificWorks);
        }
    }
}
