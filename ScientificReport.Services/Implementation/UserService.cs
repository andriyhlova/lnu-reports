using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScientificReport.DAL;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace ScientificReport.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context)
        {
           _userManager = new UserManager<ApplicationUser>(store: new UserStore<ApplicationUser>(context));
        }

        public ApplicationUser GetCurrentUser(string userName)
        {
            return _userManager.FindByNameAsync(userName).Result;
        }
    }
}