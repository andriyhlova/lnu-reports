using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction {
  public interface IThemeOfScientificWorksService {
    Task<List<ThemeOfScientificWork>> GetScientificThemes(int cathedraId);

    Task<ThemeOfScientificWork> GetScientificThemeById(int? id);

    Task AddTheme(ThemeOfScientificWork themeOfScientificWork);

    Task RemoveTheme(int themeId);

    Task SetStateModified(ThemeOfScientificWork themeOfScientificWork);

    void Dispose();
  }
}
