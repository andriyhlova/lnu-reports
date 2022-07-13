using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SRS.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetAllAsync();
    }
}
