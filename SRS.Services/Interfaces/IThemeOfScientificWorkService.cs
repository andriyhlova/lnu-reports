using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface IThemeOfScientificWorkService : IBaseCrudService<ThemeOfScientificWorkModel>
    {
        Task<IList<ThemeOfScientificWorkModel>> GetThemesForUserAsync(UserModel user);
    }
}
