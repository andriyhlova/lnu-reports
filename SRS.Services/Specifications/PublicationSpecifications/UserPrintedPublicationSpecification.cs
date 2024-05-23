using SRS.Domain.Entities;
using System;
using System.Linq;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class UserPrintedPublicationSpecification : BaseSpecification<Publication>
    {
        public UserPrintedPublicationSpecification(string userId, DateTime reportDate)
            : base(x => x.User.Any(y => y.Id == userId) && x.PrintedPublicationReport.Any(y => y.UserId == userId && y.Date.Value.Year <= reportDate.Year))
        {
            AddIncludes(x => x.User.Select(u => u.Cathedra));
        }
    }
}
