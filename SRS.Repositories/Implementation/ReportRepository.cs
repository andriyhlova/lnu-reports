using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                DeletePerformer(theme.ApplicationUserFullTime, _context);
                DeletePerformer(theme.ApplicationUserExternalPartTime, _context);
                DeletePerformer(theme.ApplicationUserLawContract, _context);

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

                var newApplicationUsersFullTime = newEntity.ThemeOfScientificWorks
                                                  .FirstOrDefault(y => y.Id == theme.Id).ApplicationUserFullTime;
                var newApplicationUsersExternalPartTime = newEntity.ThemeOfScientificWorks
                                                  .FirstOrDefault(y => y.Id == theme.Id).ApplicationUserExternalPartTime;
                var newApplicationUsersLawContract = newEntity.ThemeOfScientificWorks
                                                  .FirstOrDefault(y => y.Id == theme.Id).ApplicationUserLawContract;

                UpdatePerformer(theme.ApplicationUserFullTime, newApplicationUsersFullTime, _context);
                UpdatePerformer(theme.ApplicationUserExternalPartTime, newApplicationUsersExternalPartTime, _context);
                UpdatePerformer(theme.ApplicationUserLawContract, newApplicationUsersLawContract, _context);
            }

            var toAddThemes = newEntity.ThemeOfScientificWorks.Where(x => !existingEntity.ThemeOfScientificWorks.Any(y => y.Id == x.Id)).ToList();
            foreach (var theme in toAddThemes)
            {
                existingEntity.ThemeOfScientificWorks.Add(theme);

                AddPerformer(theme.ApplicationUserFullTime, _context);
                AddPerformer(theme.ApplicationUserExternalPartTime, _context);
                AddPerformer(theme.ApplicationUserLawContract, _context);
            }
        }

        private void DeletePerformer(ICollection<ApplicationUser> usersCollection, ApplicationDbContext context)
        {
            var usersFullTime = usersCollection.ToList();
            foreach (var user in usersFullTime)
            {
                usersCollection.Remove(user);
#pragma warning disable S1481 // Unused local variables should be removed
                var a = context.Entry(user).State;
#pragma warning restore S1481 // Unused local variables should be removed
            }
        }

        private void UpdatePerformer(
            ICollection<ApplicationUser> currentUsers,
            ICollection<ApplicationUser> newEntityUsers,
            ApplicationDbContext context)
        {
            var toDeleteApplicationUsers = currentUsers
                                            .Where(x => !newEntityUsers
                                            .Any(y => y.Id == x.Id)).ToList();

            foreach (var user in toDeleteApplicationUsers)
            {
                currentUsers.Remove(user);
#pragma warning disable S1481 // Unused local variables should be removed
                var a = context.Entry(user).State;
#pragma warning restore S1481 // Unused local variables should be removed
            }

            var toAddApplicationUsers = newEntityUsers
                                        .Where(x => !currentUsers
                                        .Any(y => y.Id == x.Id)).ToList();

            foreach (var user in toAddApplicationUsers)
            {
                currentUsers.Add(user);
                context.Entry(user).State = EntityState.Unchanged;
            }
        }

        private void AddPerformer(ICollection<ApplicationUser> usersCollection, ApplicationDbContext context)
        {
            var toAddApplicationUsers = usersCollection.ToList();

            foreach (var user in toAddApplicationUsers)
            {
                context.Entry(user).State = EntityState.Unchanged;
            }
        }
    }
}
