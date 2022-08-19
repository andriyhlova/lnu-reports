using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IReportService
    {
        Task<IList<BaseReportModel>> GetForUserAsync(UserAccountModel user, ReportFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, ReportFilterModel filterModel);

        Task<bool> ChangeState(int id, ReportState state);

        Task<ReportModel> GetUserReportAsync(string userId, int? reportId);

        Task<int> UpsertAsync<TModel>(TModel model, string currentUserId)
            where TModel : BaseModel;
    }
}
