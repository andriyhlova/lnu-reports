using System.Linq;
using SRS.Domain.Entities;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class UserPrintedPublicationSpecification : BaseSpecification<Publication>
    {
        public UserPrintedPublicationSpecification(string userId)
            : base(x => x.User.Any(y => y.Id == userId) && x.PrintedPublicationReport.Any(y => y.UserId == userId))
        {
            AddIncludes(x => x.User.Select(u => u.Cathedra));
        }
    }
}
