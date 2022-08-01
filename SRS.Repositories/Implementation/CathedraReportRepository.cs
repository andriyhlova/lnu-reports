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
            AddCollection(entity.PrintedPublicationBudgetTheme);
            AddCollection(entity.PrintedPublicationThemeInWorkTime);
            AddCollection(entity.PrintedPublicationHospDohovirTheme);
        }

        protected override void UpdateRelatedEntities(CathedraReport existingEntity, CathedraReport newEntity)
        {
            UpdateCollection(existingEntity.PrintedPublicationBudgetTheme, newEntity.PrintedPublicationBudgetTheme);
            UpdateCollection(existingEntity.PrintedPublicationThemeInWorkTime, newEntity.PrintedPublicationThemeInWorkTime);
            UpdateCollection(existingEntity.PrintedPublicationHospDohovirTheme, newEntity.PrintedPublicationHospDohovirTheme);
        }
    }
}
