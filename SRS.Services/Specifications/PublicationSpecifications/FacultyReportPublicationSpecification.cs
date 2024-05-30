using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Services.Models.FilterModels;
using System.Linq;

namespace SRS.Services.Specifications.PublicationSpecifications
{
    public class FacultyReportPublicationSpecification : BaseSpecification<Publication>
    {
        public FacultyReportPublicationSpecification(DepartmentReportPublicationFilterModel filterModel)
            : base(
                  x => x.PublicationsCathedraReport.Any(y => y.User.Cathedra.FacultyId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                  x.PatentsForInventionCathedraReport.Any(y => y.User.Cathedra.FacultyId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                  x.ApplicationsForInventionCathedraReport.Any(y => y.User.Cathedra.FacultyId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year),
                  true)
        {
            AddIncludes(
                x => x.PrintedPublicationReport.Select(y => y.User),
                x => x.RecomendedPublicationReport.Select(y => y.User),
                x => x.AcceptedToPrintPublicationReport.Select(y => y.User),
                x => x.StudentPublicationReport.Select(y => y.User),
                x => x.PatentsForInventionReport.Select(y => y.User),
                x => x.ApplicationsForInventionReport.Select(y => y.User));
            ApplyOrderByDescending(x => x.Date);
        }
    }
}
