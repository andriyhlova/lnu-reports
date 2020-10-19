using ScientificReport.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace ScientificReport.DAL.Abstraction
{
    public interface IDbContext
    {
        DbSet<AcademicStatus> AcademicStatus { get; set; }
        DbSet<ScienceDegree> ScienceDegree { get; set; }
        DbSet<Cathedra> Cathedra { get; set; }
        DbSet<Position> Position { get; set; }
        DbSet<Faculty> Faculty { get; set; }
        DbSet<Publication> Publication { get; set; }
        DbSet<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<CathedraReport> CathedraReport { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task SaveChangesAsync();
        DbEntityEntry Entry(object entity);
    }
}
