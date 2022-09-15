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
            var toAddRoles = newEntity.Roles.Where(x => !existingEntity.Roles.Any(y => y.RoleId == x.RoleId)).ToList();
            foreach (var role in toAddRoles)
            {
                existingEntity.Roles.Add(role);
            }

            var toDeleteRoles = existingEntity.Roles.Where(x => !newEntity.Roles.Any(y => y.RoleId == x.RoleId)).ToList();
            foreach (var role in toDeleteRoles)
            {
                existingEntity.Roles.Remove(role);
            }

            var toDeleteDegrees = existingEntity.Degrees.Where(x => !newEntity.Degrees.Any(y => y.Id == x.Id)).ToList();
            foreach (var degree in toDeleteDegrees)
            {
                existingEntity.Degrees.Remove(degree);
            }

            var toAddDegrees = newEntity.Degrees.Where(x => !existingEntity.Degrees.Any(y => y.Id == x.Id)).ToList();
            foreach (var degree in toAddDegrees)
            {
                existingEntity.Degrees.Add(degree);
            }

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

            var toDeleteHonoraryTitle = existingEntity.HonoraryTitles.Where(x => !newEntity.HonoraryTitles.Any(y => y.Id == x.Id)).ToList();
            foreach (var honoraryTitle in toDeleteHonoraryTitle)
            {
                existingEntity.HonoraryTitles.Remove(honoraryTitle);
            }

            var toAddHonoraryTitles = newEntity.HonoraryTitles.Where(x => !existingEntity.HonoraryTitles.Any(y => y.Id == x.Id)).ToList();
            foreach (var honoraryTitle in toAddHonoraryTitles)
            {
                _context.Entry(honoraryTitle).State = EntityState.Unchanged;
                existingEntity.HonoraryTitles.Add(honoraryTitle);
            }
        }
    }
}
