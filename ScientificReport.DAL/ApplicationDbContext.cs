using Microsoft.AspNet.Identity.EntityFramework;
using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificReport.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IDbContext
    {
        public DbSet<AcademicStatus> AcademicStatus { get; set; }
        public DbSet<ScienceDegree> ScienceDegree { get; set; }
        public DbSet<Cathedra> Cathedra { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Publication> Publication { get; set; }
        public DbSet<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<CathedraReport> CathedraReport { get; set; }
        DbEntityEntry IDbContext.Entry(object entity)
        {
            return this.Entry(entity);
        }
        async Task IDbContext.SaveChangesAsync()
        {
            await this.SaveChangesAsync();
        }
    }
}
