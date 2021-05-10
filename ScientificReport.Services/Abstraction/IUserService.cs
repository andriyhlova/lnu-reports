using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction
{
    public interface IUserService
    {
        ApplicationUser GetCurrentUser(string userName);
    }
}
