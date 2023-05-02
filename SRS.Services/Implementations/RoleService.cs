using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<RoleModel>> GetAvailableRolesAsync(UserAccountModel currentUser)
        {
            var availableRoles = await _repo.GetAllAsync();
            if (currentUser.IsInRole(RoleNames.Superadmin))
            {
                return _mapper.Map<List<RoleModel>>(availableRoles);
            }
            else if (currentUser.IsInRole(RoleNames.RectorateAdmin))
            {
                availableRoles = availableRoles
                    .Where(x => x.Name != RoleNames.Superadmin
                                && x.Name != RoleNames.RectorateAdmin)
                    .ToList();
            }
            else if (currentUser.IsInRole(RoleNames.DeaneryAdmin))
            {
                availableRoles = availableRoles
                    .Where(x => x.Name != RoleNames.Superadmin
                                && x.Name != RoleNames.RectorateAdmin
                                && x.Name != RoleNames.DeaneryAdmin
                                && x.Name != RoleNames.ThemeOfScientificWorkAdmin)
                    .ToList();
            }
            else if (currentUser.IsInRole(RoleNames.CathedraAdmin))
            {
                availableRoles = availableRoles
                    .Where(x => x.Name != RoleNames.Superadmin
                                && x.Name != RoleNames.RectorateAdmin
                                && x.Name != RoleNames.DeaneryAdmin
                                && x.Name != RoleNames.CathedraAdmin
                                && x.Name != RoleNames.ThemeOfScientificWorkAdmin)
                    .ToList();
            }
            else if (currentUser.IsInRole(RoleNames.Worker) ||
                     currentUser.IsInRole(RoleNames.ThemeOfScientificWorkAdmin) ||
                     currentUser.IsInRole(RoleNames.ExternalPartTimeEmployee))
            {
                availableRoles = new List<IdentityRole>();
            }

            return _mapper.Map<List<RoleModel>>(availableRoles);
        }
    }
}
