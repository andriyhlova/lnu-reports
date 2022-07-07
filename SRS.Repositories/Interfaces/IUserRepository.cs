using System.Threading.Tasks;
using SRS.Domain.Entities;

namespace SRS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByUsernameAsync(string username);
    }
}
