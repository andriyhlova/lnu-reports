using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Interfaces
{
    public interface IPublicationService
    {
        Task<IList<BasePublicationModel>> GetForUserAsync(UserAccountModel user, PublicationFilterModel filterModel);

        Task<int> CountForUserAsync(UserAccountModel user, PublicationFilterModel filterModel);

        Task<IList<BasePublicationModel>> GetAvailableReportPublicationsAsync(ReportPublicationFilterModel filterModel);
    }
}
