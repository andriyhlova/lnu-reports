using SRS.Services.Models.FilterModels;
using SRS.Services.Models.JournalModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IJournalService
    {
        Task<IList<JournalModel>> GetAllAsync(JournalFilterModel filterModel);

        Task<JournalModel> GetFirstOrDefaultAsync(int id);

        Task<int> CountAsync(JournalFilterModel filterModel);
    }
}
