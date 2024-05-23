using System.Linq;
using SRS.Domain.Entities;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class UserPatentSpecification : BaseSpecification<Publication>
    {
        public UserPatentSpecification(string userId)
            : base(x => x.User.Any(y => y.Id == userId) && x.PatentsForInventionReport.Any())
        {
            AddIncludes(x => x.User.Select(u => u.Cathedra));
        }
    }
}
