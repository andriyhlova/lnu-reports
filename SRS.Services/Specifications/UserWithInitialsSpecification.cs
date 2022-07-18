using System;
using System.Linq.Expressions;
using SRS.Domain.Entities;

namespace SRS.Domain.Specifications
{
    public class UserWithInitialsSpecification : BaseSpecification<ApplicationUser>
    {
        public UserWithInitialsSpecification(Expression<Func<ApplicationUser, bool>> expression)
            : base(
                  expression,
                  true)
        {
            AddIncludes(x => x.I18nUserInitials, x => x.Cathedra);
        }
    }
}
