using SRS.Domain.Enums;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IThemeOfScientificWorkService
    {
        Task<IList<BaseThemeOfScientificWorkWithFinancialsModel>> GetAsync(ThemeOfScientificWorkFilterModel filterModel);

        Task<int> CountAsync(ThemeOfScientificWorkFilterModel filterModel);

        Task<IList<BaseThemeOfScientificWorkModel>> GetActiveAsync(ThemeOfScientificWorkFilterModel filterModel, params Financial[] financials);

        Task<IList<ThemeOfScientificWorkModel>> GetActiveForCathedraReportAsync(int cathedraId, Financial financial);

        Task<Dictionary<Financial, IList<CathedraReportThemeOfScientificWorkModel>>> GetActiveForCathedraReport1Async(int cathedraId, DateTime date);

        Task<IList<BaseThemeOfScientificWorkModel>> GetGrantsForCathedraReportAsync(int cathedraId, DateTime date);

        Task<bool> ToggleActivationAsync(int id);
    }
}
