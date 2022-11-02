using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Context;
using SRS.Repositories.Interfaces;
using SRS.Repositories.Utilities;

namespace SRS.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAsync(ISpecification<ApplicationUser> specification)
        {
            return await SpecificationEvaluator<ApplicationUser>.GetQuery(_context.Set<ApplicationUser>(), specification)
                .ToListAsync();
        }

        public virtual Task<ApplicationUser> GetAsync(string id, ISpecification<ApplicationUser> specification)
        {
            return SpecificationEvaluator<ApplicationUser>.GetQuery(_context.Set<ApplicationUser>(), specification)
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            var existingEntity = await GetByIdAsync(user.Id);
            if (existingEntity == null)
            {
                return existingEntity;
            }

            UpdateRelatedEntities(existingEntity, user);
            _context.Entry(existingEntity).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                return false;
            }

            existingEntity.I18nUserInitials?.Clear();
            existingEntity.Degrees?.Clear();
            existingEntity.AcademicStatuses?.Clear();
            existingEntity.HonoraryTitles?.Clear();

            var reports = _context.Reports.Include(x => x.User).Where(x => x.User.Id == id);
            _context.Reports.RemoveRange(reports);

            var cathedraReports = _context.CathedraReport.Include(x => x.User).Where(x => x.User.Id == id);
            _context.CathedraReport.RemoveRange(cathedraReports);

            foreach (var item in _context.Users.Where(x => x.ApprovedById == id))
            {
                item.ApprovedById = null;
            }

            _context.Users.Remove(existingEntity);
            await _context.SaveChangesAsync();

            return true;
        }

        public virtual Task<int> CountAsync(ISpecification<ApplicationUser> specification)
        {
            return SpecificationEvaluator<ApplicationUser>.GetQuery(_context.Set<ApplicationUser>(), specification)
                .CountAsync();
        }

        protected void UpdateRelatedEntities(ApplicationUser existingEntity, ApplicationUser newEntity)
        {
            UpdateInitials(existingEntity, newEntity);
            UpdateRoles(existingEntity, newEntity);
            UpdateDegrees(existingEntity, newEntity);
            UpdateAcademicStatuses(existingEntity, newEntity);
            UpdateHonoraryTitles(existingEntity, newEntity);
        }

        private void UpdateInitials(ApplicationUser existingEntity, ApplicationUser newEntity)
        {
            var toDeleteInitials = existingEntity.I18nUserInitials.Where(x => !newEntity.I18nUserInitials.Any(y => y.Id == x.Id)).ToList();
            foreach (var initial in toDeleteInitials)
            {
                existingEntity.I18nUserInitials.Remove(initial);
            }

            var toAddInitials = newEntity.I18nUserInitials.Where(x => !existingEntity.I18nUserInitials.Any(y => y.Id == x.Id)).ToList();
            foreach (var initial in toAddInitials)
            {
                existingEntity.I18nUserInitials.Add(initial);
            }

            var toUpdateInitials = existingEntity.I18nUserInitials.Where(x => newEntity.I18nUserInitials.Any(y => y.Id == x.Id)).ToList();
            foreach (var initial in toUpdateInitials)
            {
                var newInitial = newEntity.I18nUserInitials.FirstOrDefault(y => y.Id == initial.Id);
                initial.FirstName = newInitial.FirstName;
                initial.LastName = newInitial.LastName;
                initial.FathersName = newInitial.FathersName;
            }
        }

        private void UpdateRoles(ApplicationUser existingEntity, ApplicationUser newEntity)
        {
            var toDeleteRoles = existingEntity.Roles.Where(x => !newEntity.Roles.Any(y => y.RoleId == x.RoleId)).ToList();
            foreach (var role in toDeleteRoles)
            {
                existingEntity.Roles.Remove(role);
            }

            var toAddRoles = newEntity.Roles.Where(x => !existingEntity.Roles.Any(y => y.RoleId == x.RoleId)).ToList();
            foreach (var role in toAddRoles)
            {
                existingEntity.Roles.Add(role);
            }
        }

        private void UpdateDegrees(ApplicationUser existingEntity, ApplicationUser newEntity)
        {
            var toDeleteDegrees = existingEntity.Degrees.Where(x => !newEntity.Degrees.Any(y => y.Id == x.Id)).ToList();
            foreach (var degree in toDeleteDegrees)
            {
                existingEntity.Degrees.Remove(degree);
#pragma warning disable S1481 // Unused local variables should be removed
                var a = _context.Entry(degree).State;
#pragma warning restore S1481 // Unused local variables should be removed
            }

            var toAddDegrees = newEntity.Degrees.Where(x => !existingEntity.Degrees.Any(y => y.Id == x.Id)).ToList();
            foreach (var degree in toAddDegrees)
            {
                existingEntity.Degrees.Add(degree);
            }
        }

        private void UpdateAcademicStatuses(ApplicationUser existingEntity, ApplicationUser newEntity)
        {
            var toDeleteAcademicStatuses = existingEntity.AcademicStatuses.Where(x => !newEntity.AcademicStatuses.Any(y => y.Id == x.Id)).ToList();
            foreach (var academicStatus in toDeleteAcademicStatuses)
            {
                existingEntity.AcademicStatuses.Remove(academicStatus);
            }

            var toAddAcademicStatuses = newEntity.AcademicStatuses.Where(x => !existingEntity.AcademicStatuses.Any(y => y.Id == x.Id)).ToList();
            foreach (var academicStatus in toAddAcademicStatuses)
            {
                existingEntity.AcademicStatuses.Add(academicStatus);
            }
        }

        private void UpdateHonoraryTitles(ApplicationUser existingEntity, ApplicationUser newEntity)
        {
            var toDeleteHonoraryTitle = existingEntity.HonoraryTitles.Where(x => !newEntity.HonoraryTitles.Any(y => y.Id == x.Id)).ToList();
            foreach (var honoraryTitle in toDeleteHonoraryTitle)
            {
                existingEntity.HonoraryTitles.Remove(honoraryTitle);
            }

            var toAddHonoraryTitles = newEntity.HonoraryTitles.Where(x => !existingEntity.HonoraryTitles.Any(y => y.Id == x.Id)).ToList();
            foreach (var honoraryTitle in toAddHonoraryTitles)
            {
                existingEntity.HonoraryTitles.Add(honoraryTitle);
            }
        }
    }
}
