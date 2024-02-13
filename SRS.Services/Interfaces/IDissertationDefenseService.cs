using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IDissertationDefenseService
    {
        Task<IList<DissertationDefenseModel>> GetAsync(DissertationDefenseFilterModel filterModel);

        Task<int> CountAsync(DissertationDefenseFilterModel filterModel);
    }
}
