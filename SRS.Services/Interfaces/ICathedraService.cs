using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface ICathedraService
    {
        Task<IList<CathedraModel>> GetByFacultyAsync(int? facultyId);

        Task<IList<CathedraModel>> GetAllAsync(FacultyFilterModel filterModel);

        Task<int> CountAsync(FacultyFilterModel filterModel);
    }
}
