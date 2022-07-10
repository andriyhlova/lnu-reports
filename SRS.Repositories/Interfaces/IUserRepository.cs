using System.Threading.Tasks;
using SRS.Domain.Entities;

namespace SRS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);

        Task<ApplicationUser> GetByUsernameAsync(string username);

        Task<ApplicationUser> UpdateAsync(ApplicationUser user);
    }
}
