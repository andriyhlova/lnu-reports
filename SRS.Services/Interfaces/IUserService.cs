using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserAccountModel> GetAccountInfoByIdAsync(string id);

        Task<UserInfoModel> GetUserInfoByIdAsync(string id);

        Task<UserInfoModel> UpdateAsync(UserInfoModel user);
    }
}
