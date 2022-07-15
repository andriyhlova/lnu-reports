using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface IThemeOfScientificWorkService
    {
        Task<int> AddAsync(UserAccountModel user, ThemeOfScientificWorkModel model);

        Task<ThemeOfScientificWorkModel> UpdateAsync(UserAccountModel user, ThemeOfScientificWorkModel model);

        Task<IList<ThemeOfScientificWorkModel>> GetThemesForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel);

        Task<int> CountThemesForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel);
    }
}
