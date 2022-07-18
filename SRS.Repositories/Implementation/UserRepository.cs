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

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
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
    }
}
