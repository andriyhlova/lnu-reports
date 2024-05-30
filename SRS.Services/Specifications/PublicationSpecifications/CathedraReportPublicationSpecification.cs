using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;
using System.Linq;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class CathedraReportPublicationSpecification : BaseSpecification<Publication>
    {
        public CathedraReportPublicationSpecification(DepartmentReportPublicationFilterModel filterModel)
            : base(
                  x => x.PrintedPublicationReport.Any(y => y.User.CathedraId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                       x.RecomendedPublicationReport.Any(y => y.User.CathedraId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                  x.AcceptedToPrintPublicationReport.Any(y => y.User.CathedraId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                  x.StudentPublicationReport.Any(y => y.User.CathedraId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                  x.PatentsForInventionReport.Any(y => y.User.CathedraId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year) ||
                  x.ApplicationsForInventionReport.Any(y => y.User.CathedraId == filterModel.DepartmentId && y.Date.Value.Year == filterModel.Date.Year),
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
