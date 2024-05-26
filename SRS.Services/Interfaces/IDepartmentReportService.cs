using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.DepartmentReportModels;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IDepartmentReportService
    {
        Task<IList<BaseDepartmentReportModel>> GetForUserAsync(UserAccountModel user, DepartmentReportFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, DepartmentReportFilterModel filterModel);

        Task<DepartmentReportModel> GetUserDepartmentReportAsync(string userId, int? reportId);

        Task<int> UpsertAsync<TModel>(TModel model, string currentUserId)
            where TModel : BaseModel;

        Task<bool> ChangeState(int id, ReportState state);
    }
}
