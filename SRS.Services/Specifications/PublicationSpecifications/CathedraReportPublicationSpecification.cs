using System.Linq;
using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class CathedraReportPublicationSpecification : BaseSpecification<Publication>
    {
        public CathedraReportPublicationSpecification(CathedraReportPublicationFilterModel filterModel)
            : base(
                  x => x.PrintedPublicationReport.Any(y => y.User.CathedraId == filterModel.CathedraId && y.ThemeOfScientificWorks.Any(z => z.Financial == filterModel.Financial) && y.IsSigned && y.IsConfirmed),
                  true)
        {
            AddIncludes(x => x.PrintedPublicationReport.Select(y => y.User), x => x.PrintedPublicationReport.Select(y => y.ThemeOfScientificWorks));
            ApplyOrderByDescending(x => x.Date);
        }
    }
}
