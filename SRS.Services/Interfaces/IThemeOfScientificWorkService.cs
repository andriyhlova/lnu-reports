using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IThemeOfScientificWorkService
    {
        Task<int> AddAsync(UserAccountModel user, ThemeOfScientificWorkModel model);

        Task<ThemeOfScientificWorkModel> UpdateAsync(UserAccountModel user, ThemeOfScientificWorkModel model);

        Task<IList<ThemeOfScientificWorkModel>> GetForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel);
    }
}
