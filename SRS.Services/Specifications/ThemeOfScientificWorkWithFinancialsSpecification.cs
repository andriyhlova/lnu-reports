using System;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class ThemeOfScientificWorkWithFinancialsSpecification : ThemeOfScientificWorkSpecification
    {
        public ThemeOfScientificWorkWithFinancialsSpecification(ThemeOfScientificWorkFilterModel filterModel, Expression<Func<ThemeOfScientificWork, bool>> expression)
            : base(filterModel, expression)
        {
            AddInclude(x => x.ThemeOfScientificWorkFinancials);
        }
    }
}
