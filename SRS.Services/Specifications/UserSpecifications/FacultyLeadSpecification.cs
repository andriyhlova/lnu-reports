using SRS.Domain.Entities;
using SRS.Services.Models.Constants;

namespace SRS.Domain.Specifications.UserSpecifications
{
    public class FacultyLeadSpecification : BaseSpecification<ApplicationUser>
    {
        public FacultyLeadSpecification(int? facultyId)
            : base(
                  x => x.Cathedra.FacultyId == facultyId && x.Position.Value == PositionNames.FacultyLead,
                  true)
        {
            AddIncludes(x => x.I18nUserInitials, x => x.ScienceDegree);
        }
    }
}
