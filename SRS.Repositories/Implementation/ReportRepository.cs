using System.Data.Entity;
using System.Linq;
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
            AddCollection(entity.StudentPublication);
            AddCollection(entity.PrintedPublication);
            AddCollection(entity.RecomendedPublication);
            AddCollection(entity.AcceptedToPrintPublication);
            AddCollection(entity.ApplicationsForInvention);
            AddCollection(entity.PatentsForInvention);
        }

        protected override void UpdateRelatedEntities(Report existingEntity, Report newEntity)
        {
            UpdateCollection(existingEntity.StudentPublication, newEntity.StudentPublication);
            UpdateCollection(existingEntity.PrintedPublication, newEntity.PrintedPublication);
            UpdateCollection(existingEntity.RecomendedPublication, newEntity.RecomendedPublication);
            UpdateCollection(existingEntity.AcceptedToPrintPublication, newEntity.AcceptedToPrintPublication);
            UpdateCollection(existingEntity.ApplicationsForInvention, newEntity.ApplicationsForInvention);
            UpdateCollection(existingEntity.PatentsForInvention, newEntity.PatentsForInvention);
            UpdateThemes(existingEntity, newEntity);
        }

        private void UpdateThemes(Report existingEntity, Report newEntity)
        {
            var toDeleteThemes = existingEntity.ThemeOfScientificWorks.Where(x => !newEntity.ThemeOfScientificWorks.Any(y => y.Id == x.Id)).ToList();
            foreach (var theme in toDeleteThemes)
            {
                _context.Entry(theme).State = EntityState.Deleted;
            }

            var toUpdateThemes = existingEntity.ThemeOfScientificWorks.Where(x => newEntity.ThemeOfScientificWorks.Any(y => y.Id == x.Id)).ToList();
            foreach (var theme in toUpdateThemes)
            {
                var newInitial = newEntity.ThemeOfScientificWorks.FirstOrDefault(y => y.Id == theme.Id);
                theme.Description = newInitial.Description;
                theme.Resume = newInitial.Resume;
                theme.Publications = newInitial.Publications;
                theme.DefendedDissertation = newInitial.DefendedDissertation;
            }

            var toAddThemes = newEntity.ThemeOfScientificWorks.Where(x => !existingEntity.ThemeOfScientificWorks.Any(y => y.Id == x.Id)).ToList();
            foreach (var theme in toAddThemes)
            {
                existingEntity.ThemeOfScientificWorks.Add(theme);
            }
        }
    }
}
