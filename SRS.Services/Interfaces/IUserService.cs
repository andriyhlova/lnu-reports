using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IUserService<TUserModel>
        where TUserModel : BaseUserModel
    {
        Task<TUserModel> GetByIdAsync(string id);

        Task<IList<TUserModel>> GetForUserAsync(UserAccountModel user, UserFilterModel filterModel);

        Task<TUserModel> UpdateAsync(TUserModel user, string approvedById = null);

        Task<bool> DeleteAsync(string id);

        Task<int> CountForUserAsync(UserAccountModel user, UserFilterModel filterModel);
    }
}
