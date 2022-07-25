using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface IReportService
    {
        Task<IList<BaseReportModel>> GetReportsForUserAsync(UserAccountModel user, ReportFilterModel filterModel);

        Task<int> CountReportsForUserAsync(UserAccountModel user, ReportFilterModel filterModel);

        Task<bool> SignAsync(int id);

        Task<bool> ConfirmAsync(int id);

        Task<bool> ReturnAsync(int id);
    }
}
