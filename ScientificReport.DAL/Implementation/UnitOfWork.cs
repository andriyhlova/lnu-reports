using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Specifications;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ScientificReport.DAL.Repositories.Interfaces;
using ScientificReport.DAL.Repositories.Realizations;

namespace ScientificReport.DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAcademicStatusRepository AcademicStatuses { get; }

        public IApplicationUserRepository Users { get; }

        public ICathedraRepository Cathedras { get; }

        public IFacultyRepository Faculties { get; }

        public IPositionRepository Positions { get; }

        public IPublicationRepository Publications { get; }
        
        public IScienceDegreeRepository ScienceDegrees { get; }

        public IThemeOfScientificWorkRepository ThemeOfScientificWorks { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            AcademicStatuses = new AcademicStatusRepository(context);
            Users = new ApplicationUserRepository(context);
            Cathedras = new CathedraRepository(context);
            Faculties = new FacultyRepository(context);
            Positions = new PositionRepository(context);
            Publications = new PublicationRepository(context);
            ScienceDegrees = new ScienceDegreeRepository(context);
            ThemeOfScientificWorks = new ThemeOfScientificWorkRepository(context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
