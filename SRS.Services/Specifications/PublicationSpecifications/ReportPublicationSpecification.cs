using System.Linq;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class ReportPublicationSpecification : BaseSpecification<Publication>
    {
        public ReportPublicationSpecification(ReportPublicationFilterModel filterModel)
            : base(
                  x => x.User.Any(y => y.Id == filterModel.UserId)
                        && !x.StudentPublicationReport.Any(y => y.UserId == filterModel.UserId && (y.State == ReportState.Signed || y.State == ReportState.Confirmed))
                        && !x.PrintedPublicationReport.Any(y => y.UserId == filterModel.UserId && (y.State == ReportState.Signed || y.State == ReportState.Confirmed))
                        && !x.ApplicationsForInventionReport.Any(y => y.UserId == filterModel.UserId && (y.State == ReportState.Signed || y.State == ReportState.Confirmed))
                        && !x.PatentsForInventionReport.Any(y => y.UserId == filterModel.UserId && (y.State == ReportState.Signed || y.State == ReportState.Confirmed))
                        && (filterModel.From == null || x.Date >= filterModel.From)
                        && (filterModel.To == null || x.Date <= filterModel.To),
                  true)
        {
            AddIncludes(x => x.User, x => x.PrintedPublicationReport);
            ApplyOrderByDescending(x => x.Date);
        }
    }
}
