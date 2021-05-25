using ScientificReport.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Repositories.Interfaces;

namespace ScientificReport.DAL.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IAcademicStatusRepository AcademicStatuses { get; }

        IApplicationUserRepository Users { get; }

        ICathedraRepository Cathedras { get; }

        IFacultyRepository Faculties { get; }

        IPositionRepository Positions { get; }

        IPublicationRepository Publications { get; }

        IScienceDegreeRepository ScienceDegrees { get; }

        IThemeOfScientificWorkRepository ThemeOfScientificWorks { get; }
        ICathedraReportRepository CathedraReports { get; }
        IReportRepository Reports { get; }
        int SaveChanges();

        void Dispose();
    }
}
