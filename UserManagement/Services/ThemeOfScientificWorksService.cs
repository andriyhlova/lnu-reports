using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ScientificReport.DAL;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Services {
  public class ThemeOfScientificWorksService: IThemeOfScientificWorksService {
    private ApplicationDbContext db;

    public ThemeOfScientificWorksService() {
      db = new ApplicationDbContext();
    }
   
    public async Task<List<ThemeOfScientificWork>> GetScientificThemes(int cathedraId) {
      return await db.ThemeOfScientificWork.Where(x => x.Cathedra.ID == cathedraId).ToListAsync();
    }

    public async Task<ThemeOfScientificWork> GetScientificThemeById(int? id) {
      return await db.ThemeOfScientificWork.FindAsync(id);
    }

    public async Task AddTheme(ThemeOfScientificWork themeOfScientificWork) {
      db.ThemeOfScientificWork.Add(themeOfScientificWork);
      await db.SaveChangesAsync();
    }

    public async Task RemoveTheme(int themeId) {
      var theme = await GetScientificThemeById(themeId);
      db.ThemeOfScientificWork.Remove(theme);
      await db.SaveChangesAsync();
    }

    public async Task SetStateModified(ThemeOfScientificWork themeOfScientificWork) {
      db.Entry(themeOfScientificWork).State = EntityState.Modified;
      await db.SaveChangesAsync();
    }

    public void Dispose() {
      db.Dispose();
    }
  }
}