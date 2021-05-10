using System.Linq;
using ScientificReport.DAL;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Services {
  public class CathedraService: ICathedraService {
    private ApplicationDbContext db;

    public CathedraService() {
      db = new ApplicationDbContext();
    }

    public Cathedra GetCathedraById(int cathedraId) {
      return db.Cathedra.Where(x => x.ID == cathedraId).First();
    }
  }
}