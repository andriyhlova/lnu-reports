using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ReportModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface ICathedraReportService
    {
        Task<IList<BaseCathedraReportModel>> GetForUserAsync(UserAccountModel user, CathedraReportFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, CathedraReportFilterModel filterModel);

        Task<CathedraReportModel> GetUserCathedraReportAsync(string userId, int? reportId);

        Task<int> UpsertAsync<TModel>(TModel model, string currentUserId)
            where TModel : BaseModel;

        Task<bool> ChangeState(int id, ReportState state);
    }
}
