using System.Linq;
using ScientificReport.DAL;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Services {
  public class UserService: IUserService {
    private ApplicationDbContext db;

    public UserService() {
      db = new ApplicationDbContext();
    }

    public ApplicationUser GetCurrentUser(string userName) {
      return db.Users.Where(x => x.UserName == userName).First();
    }
  }
}