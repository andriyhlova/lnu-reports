using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Domain.Enums;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;

namespace SRS.Services.Interfaces
{
    public interface IThemeOfScientificWorkService
    {
        Task<IList<BaseThemeOfScientificWorkModel>> GetAsync(DepartmentFilterModel filterModel);

        Task<int> CountAsync(DepartmentFilterModel filterModel);

        Task<IList<BaseThemeOfScientificWorkModel>> GetActiveAsync(DepartmentFilterModel filterModel, params Financial[] financials);

        Task<IList<ThemeOfScientificWorkModel>> GetActiveForCathedraReportAsync(int cathedraId, Financial financial);
    }
}
