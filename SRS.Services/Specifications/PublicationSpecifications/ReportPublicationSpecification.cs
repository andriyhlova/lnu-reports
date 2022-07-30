using System.Linq;
using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class ReportPublicationSpecification : BaseSpecification<Publication>
    {
        public ReportPublicationSpecification(ReportPublicationFilterModel filterModel)
            : base(
                  x => x.User.Any(y => y.Id == filterModel.UserId)
                        && !x.PrintedPublicationReport.Any(y => y.UserId == filterModel.UserId && (y.IsSigned || y.IsConfirmed))
                        && (filterModel.From == null || x.Date >= filterModel.From)
                        && (filterModel.To == null || x.Date <= filterModel.To),
                  true)
        {
            AddIncludes(x => x.User, x => x.PrintedPublicationReport);
            ApplyOrderByDescending(x => x.Date);
        }
    }
}
