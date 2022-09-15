using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SRS.Domain.Entities;

namespace SRS.Repositories.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Degree> Degree { get; set; }

        public DbSet<AcademicStatus> AcademicStatus { get; set; }

        public DbSet<HonoraryTitle> HonoraryTitles { get; set; }

        public DbSet<Cathedra> Cathedra { get; set; }

        public DbSet<Position> Position { get; set; }

        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Publication> Publication { get; set; }

        public DbSet<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<CathedraReport> CathedraReport { get; set; }

        public DbSet<Journal> Journals { get; set; }

        public DbSet<JournalType> JournalTypes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}