﻿using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Implementation;
using ScientificReport.DAL.Models;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Repositories.Realizations
{
    public class ApplicationUserRepository: BaseRepository<ApplicationUser,string>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbContext context) : base(context)
        {
        }
    }
}