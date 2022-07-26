using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetAvailableRolesAsync(UserAccountModel currentUser);
    }
}
