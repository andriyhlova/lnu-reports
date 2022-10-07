using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.JournalModels;

namespace SRS.Services.Interfaces
{
    public interface IJournalService
    {
        Task<IList<JournalModel>> GetAllAsync(JournalFilterModel filterModel);

        Task<int> CountAsync(JournalFilterModel filterModel);
    }
}
