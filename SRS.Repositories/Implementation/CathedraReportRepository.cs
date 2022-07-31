using System.Data.Entity;
using SRS.Domain.Entities;
using SRS.Repositories.Context;

namespace SRS.Repositories.Implementations
{
    public class CathedraReportRepository : BaseRepository<CathedraReport>
    {
        public CathedraReportRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void AddRelatedEntities(CathedraReport entity)
        {
            foreach (var user in entity.PrintedPublicationBudgetTheme)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }

            foreach (var user in entity.PrintedPublicationThemeInWorkTime)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }

            foreach (var user in entity.PrintedPublicationHospDohovirTheme)
            {
                _context.Entry(user).State = EntityState.Unchanged;
            }
        }

        protected override void UpdateRelatedEntities(CathedraReport existingEntity, CathedraReport newEntity)
        {
            UpdateCollection(existingEntity.PrintedPublicationBudgetTheme, newEntity.PrintedPublicationBudgetTheme);
            UpdateCollection(existingEntity.PrintedPublicationThemeInWorkTime, newEntity.PrintedPublicationThemeInWorkTime);
            UpdateCollection(existingEntity.PrintedPublicationHospDohovirTheme, newEntity.PrintedPublicationHospDohovirTheme);
        }
    }
}
