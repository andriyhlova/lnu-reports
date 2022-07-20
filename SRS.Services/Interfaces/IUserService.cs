using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface IUserService<TUserModel>
        where TUserModel : BaseUserModel
    {
        Task<TUserModel> GetByIdAsync(string id);

        Task<IList<TUserModel>> GetAsync(UserAccountModel user, DepartmentFilterModel filterModel);

        Task<TUserModel> UpdateAsync(TUserModel user, string approvedById = null);

        Task<bool> DeleteAsync(string id);

        Task<int> CountAsync(UserAccountModel user, DepartmentFilterModel filterModel);
    }
}
