﻿using System.Data.Entity;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Repositories.Context;
using SRS.Repositories.Interfaces;

namespace SRS.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
