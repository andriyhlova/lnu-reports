using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetByUsernameAsync(string username);
    }
}
