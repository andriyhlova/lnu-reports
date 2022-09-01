using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Interfaces
{
    public interface IAcademicStatusService
    {
        Task<IList<AcademicStatusModel>> GetAllAsync(BaseFilterModel filterModel);
    }
}
