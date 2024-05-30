using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface ISpecializationService
    {
        Task<IList<SpecializationModel>> GetAllAsync(BaseFilterModel filterModel);

        Task<int> CountAsync(BaseFilterModel filterModel);
    }
}
