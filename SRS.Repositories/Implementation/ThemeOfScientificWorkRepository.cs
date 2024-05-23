using System.Data.Entity;
using System.Linq;
using SRS.Domain.Entities;
using SRS.Repositories.Context;

namespace SRS.Repositories.Implementations
{
    public class ThemeOfScientificWorkRepository : BaseRepository<ThemeOfScientificWork>
    {
        public ThemeOfScientificWorkRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void UpdateRelatedEntities(ThemeOfScientificWork existingEntity, ThemeOfScientificWork newEntity)
        {
            UpdateFinancials(existingEntity, newEntity);
            UpdateCathedras(existingEntity, newEntity);
            UpdateSupervisors(existingEntity, newEntity);
        }

        private void UpdateFinancials(ThemeOfScientificWork existingEntity, ThemeOfScientificWork newEntity)
        {
            var toDelete = existingEntity.ThemeOfScientificWorkFinancials.Where(x => !newEntity.ThemeOfScientificWorkFinancials.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toDelete)
            {
                _context.Entry(item).State = EntityState.Deleted;
                existingEntity.ThemeOfScientificWorkFinancials.Remove(item);
            }

            var toAdd = newEntity.ThemeOfScientificWorkFinancials.Where(x => !existingEntity.ThemeOfScientificWorkFinancials.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toAdd)
            {
                _context.Entry(item).State = EntityState.Added;
                existingEntity.ThemeOfScientificWorkFinancials.Add(item);
            }
        }

        private void UpdateCathedras(ThemeOfScientificWork existingEntity, ThemeOfScientificWork newEntity)
        {
            var toDelete = existingEntity.ThemeOfScientificWorkCathedras.Where(x => !newEntity.ThemeOfScientificWorkCathedras.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toDelete)
            {
                _context.Entry(item).State = EntityState.Deleted;
                existingEntity.ThemeOfScientificWorkCathedras.Remove(item);
            }

            var toAdd = newEntity.ThemeOfScientificWorkCathedras.Where(x => !existingEntity.ThemeOfScientificWorkCathedras.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toAdd)
            {
                _context.Entry(item).State = EntityState.Added;
                existingEntity.ThemeOfScientificWorkCathedras.Add(item);
            }
        }

        private void UpdateSupervisors(ThemeOfScientificWork existingEntity, ThemeOfScientificWork newEntity)
        {
            var toDelete = existingEntity.ThemeOfScientificWorkSupervisors.Where(x => !newEntity.ThemeOfScientificWorkSupervisors.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toDelete)
            {
                _context.Entry(item).State = EntityState.Deleted;
                existingEntity.ThemeOfScientificWorkSupervisors.Remove(item);
            }

            var toAdd = newEntity.ThemeOfScientificWorkSupervisors.Where(x => !existingEntity.ThemeOfScientificWorkSupervisors.Any(y => y.Id == x.Id)).ToList();
            foreach (var item in toAdd)
            {
                _context.Entry(item).State = EntityState.Added;
                existingEntity.ThemeOfScientificWorkSupervisors.Add(item);
            }
        }
    }
}
