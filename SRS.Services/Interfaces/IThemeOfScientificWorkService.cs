using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Domain.Enums;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IThemeOfScientificWorkService
    {
        Task<IList<ThemeOfScientificWorkModel>> GetForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, DepartmentFilterModel filterModel);

        Task<IList<ThemeOfScientificWorkModel>> GetActiveForFacultyAsync(int? facultyId);

        Task<IList<ThemeOfScientificWorkModel>> GetActiveForCathedraReportAsync(int cathedraId, Financial financial);
    }
}
