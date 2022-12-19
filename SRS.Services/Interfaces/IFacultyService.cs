using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface IFacultyService
    {
        Task<IList<FacultyModel>> GetAllAsync(BaseFilterModel filterModel);

        Task<int> CountAsync(BaseFilterModel filterModel);
    }
}
