using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SRS.Repositories.Context;
using SRS.Repositories.Interfaces;

namespace SRS.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IdentityRole>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
