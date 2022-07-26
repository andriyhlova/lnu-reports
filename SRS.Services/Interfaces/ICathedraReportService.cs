using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface ICathedraReportService
    {
        Task<IList<BaseCathedraReportModel>> GetForUserAsync(UserAccountModel user, CathedraReportFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, CathedraReportFilterModel filterModel);
    }
}
