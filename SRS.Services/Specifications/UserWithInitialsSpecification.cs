using System;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Services.Extensions;

namespace SRS.Domain.Specifications
{
    public class UserWithInitialsSpecification : BaseSpecification<ApplicationUser>
    {
        public UserWithInitialsSpecification(int? facultyId, int? cathedraId, Expression<Func<ApplicationUser, bool>> expression)
            : base(
                  expression.AndAlso(x => (facultyId == null || x.Cathedra.FacultyId == facultyId) &&
                                          (cathedraId == null || x.CathedraId == cathedraId)),
                  true)
        {
            AddIncludes(x => x.I18nUserInitials, x => x.Cathedra);
        }
    }
}
