using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Implementations
{
    public class RoleActionService : IRoleActionService
    {
        public async Task<TItem> TakeRoleActionAsync<TItem>(UserAccountModel user, Dictionary<string, Func<Task<TItem>>> actions)
        {
            var roleName = string.Empty;
            if (user.IsInRole(RoleNames.Superadmin))
            {
                roleName = RoleNames.Superadmin;
            }
            else if (user.IsInRole(RoleNames.RectorateAdmin))
            {
                roleName = RoleNames.RectorateAdmin;
            }
            else if (user.IsInRole(RoleNames.DeaneryAdmin))
            {
                roleName = RoleNames.DeaneryAdmin;
            }
            else if (user.IsInRole(RoleNames.CathedraAdmin))
            {
                roleName = RoleNames.CathedraAdmin;
            }
            else if (user.IsInRole(RoleNames.Worker))
            {
                roleName = RoleNames.Worker;
            }

            var hasAction = actions.TryGetValue(roleName, out var action);

            return hasAction ? await action() : default;
        }
    }
}
