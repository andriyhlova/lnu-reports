using SRS.Domain.Entities;
using SRS.Services.Models.Constants;

namespace SRS.Domain.Specifications.UserSpecifications
{
    public class CathedraLeadSpecification : BaseSpecification<ApplicationUser>
    {
        public CathedraLeadSpecification(int? cathedraId)
            : base(
                  x => x.CathedraId == cathedraId && x.Position.Value == PositionNames.CathedraLead,
                  true)
        {
            AddIncludes(x => x.I18nUserInitials, x => x.ScienceDegree);
        }
    }
}
