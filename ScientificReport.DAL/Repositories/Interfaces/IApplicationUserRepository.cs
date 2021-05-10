using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Models;

namespace ScientificReport.DAL.Repositories.Interfaces
{
    public interface IApplicationUserRepository: IGenericRepository<ApplicationUser, string>
    {
    }
}
