using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IReportService
    {
        Task<IList<BaseReportModel>> GetForUserAsync(UserAccountModel user, ReportFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, ReportFilterModel filterModel);

        Task<bool> SignAsync(int id);

        Task<bool> ConfirmAsync(int id);

        Task<bool> ReturnAsync(int id);
    }
}
