using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface ICathedraService : IBaseCrudService<CathedraModel>
    {
        Task<IList<CathedraModel>> GetByFacultyAsync(int? facultyId);
    }
}
