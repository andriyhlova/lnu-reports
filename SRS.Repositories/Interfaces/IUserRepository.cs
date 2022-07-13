using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;

namespace SRS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);

        Task<ApplicationUser> GetByUsernameAsync(string username);

        Task<List<ApplicationUser>> GetAsync(ISpecification<ApplicationUser> specification);

        Task<int> CountAsync(ISpecification<ApplicationUser> specification);

        Task<ApplicationUser> UpdateAsync(ApplicationUser user);

        Task<bool> DeleteAsync(string id);
    }
}
