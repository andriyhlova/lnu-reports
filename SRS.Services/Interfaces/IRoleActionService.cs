using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface IRoleActionService
    {
        Task<TItem> TakeRoleActionAsync<TItem>(UserAccountModel user, Dictionary<string, Func<Task<TItem>>> actions);
    }
}
