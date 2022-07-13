using System;
using System.Linq.Expressions;
using SRS.Domain.Entities;

namespace SRS.Domain.Specifications
{
    public class ThemeOfScientificWorkSpecification : BaseFilterSpecification<ThemeOfScientificWork>
    {
        public ThemeOfScientificWorkSpecification(int? skip, int? take, Expression<Func<ThemeOfScientificWork, bool>> expression)
            : base(skip, take, expression, true)
        {
            AddInclude(x => x.Cathedra);
            ApplyOrderByDescending(x => x.PeriodTo);
        }
    }
}
