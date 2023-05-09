using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface ICathedraService
    {
        Task<IList<CathedraModel>> GetByFacultyAsync(int? facultyId);

        Task<IList<CathedraModel>> GetAllAsync(BaseFilterModel filterModel);
    }
}
