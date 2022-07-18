using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface IPublicationService
    {
        Task<IList<BasePublicationModel>> GetPublicationsForUserAsync(UserAccountModel user, PublicationFilterModel filterModel);

        Task<int> CountPublicationsForUserAsync(UserAccountModel user, PublicationFilterModel filterModel);
    }
}
